using JobFinder.Application.Features.Profiles.Commands.UpdateCandidateProfile;
using JobFinder.Application.Features.Profiles.Queries.GetCandidateProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobFinder.API.Controllers
{
    [Route("profile")]
    [ApiController]
    public class CandidateProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpAccessor;

        public CandidateProfileController(
            IMediator mediator,
            IHttpContextAccessor httpAccessor)
        {
            _mediator = mediator;
            _httpAccessor = httpAccessor;
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(UpdateCandidateProfileCommand command)
        {
            var userId = _httpAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var commandCopy = command with { UserId = Guid.Parse(userId!) };

            var id = await _mediator.Send(commandCopy);
            return Ok(id);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<CandidateProfileDto>> GetById(Guid id)
        {
            var profile = await _mediator.Send(new GetCandidateProfileQuery(id));
            return Ok(profile);
        }
    }
}
