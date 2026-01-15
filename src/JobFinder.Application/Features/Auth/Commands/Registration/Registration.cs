using AutoMapper;
using JobFinder.Application.Interfaces;
using JobFinder.Domain.Entities;
using JobFinder.Domain.Enums;
using JobFinder.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Features.Auth.Commands.Registration
{
    public record RegisterResponse(Guid Id, string Token);

    public record RegistrationCommand(
        string Email,
        string Password,
        UserType UserType
        ) : IRequest<RegisterResponse>;

    public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, RegisterResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher _hasher;
        private readonly IJwtProvider _jwtProvider;

        public RegistrationCommandHandler(
            IApplicationDbContext context,
            IPasswordHasher hasher,
            IJwtProvider jwtProvider)
        {
            _context = context;
            _hasher = hasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<RegisterResponse> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            var normalizedEmail = request.Email.Trim().ToLowerInvariant();

            var isTaken = await _context.Users.AnyAsync(x => x.Email == normalizedEmail, cancellationToken);

            if (isTaken)
            {
                throw new AlreadyExistsException("Account already exists.");
            }

            var user = new User
            {
                Email = normalizedEmail,
                PasswordHash = _hasher.HashPassword(request.Password),
                UserType = request.UserType
            };

            user.InitializeProfile();

            _context.Users.Add(user);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException)
            {
                throw new AlreadyExistsException("Account already exists.");
            }
            

            var token = _jwtProvider.GenerateToken(user);

            return new RegisterResponse(user.Id, token);
        }
    }
}
