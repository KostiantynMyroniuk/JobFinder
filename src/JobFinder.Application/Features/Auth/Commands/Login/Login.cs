using JobFinder.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Features.Auth.Commands.Login
{
    public record LoginResponse(Guid Id, string Token);

    public record LoginCommand(
        string Email,
        string Password
        ) : IRequest<LoginResponse>;

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher _hasher;
        private readonly IJwtProvider _jwtProvider;

        public LoginCommandHandler(
            IApplicationDbContext context,
            IPasswordHasher hasher,
            IJwtProvider jwtProvider)
        {
            _context = context;
            _hasher = hasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var normalizedEmail = request.Email.Trim().ToLowerInvariant();

            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == normalizedEmail, cancellationToken);

            if (user is null || !_hasher.Verify(request.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            return new LoginResponse(user.Id, _jwtProvider.GenerateToken(user));
        }
    }
}
