using JobFinder.Application.Features.Profiles.Queries.GetCandidateProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.API.Controllers
{
    [Route("profile/me")]
    [ApiController]
    public class MyProfileController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly IMediator _mediator;

        public MyProfileController(
            IMediator mediator,
            IHttpContextAccessor httpAccessor)
        {
            _mediator = mediator;
            _httpAccessor = httpAccessor;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<CandidateProfileDto>> GetMyProfile()
        {
            var userId = _httpAccessor.HttpContext?.User.FindFirst("Sub")?.Value;
            var userType = _httpAccessor.HttpContext?.User.FindFirst("user_type")?.Value;

            var query = userType switch
            {
                "Candidate" => new GetCandidateProfileQuery(Guid.Parse(userId!)),
                ////////////////////////////
                _ => throw new InvalidDataException()
            };

            var profile = await _mediator.Send(query);

            return Ok(profile);
        }
    }
}
