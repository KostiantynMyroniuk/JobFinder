using JobFinder.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Events
{
    public record UserRegisteredEvent(Guid Id, UserType UserType) : INotification;
    
}
