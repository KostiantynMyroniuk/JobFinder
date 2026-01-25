using JobFinder.Application.Features.Vacancies.Commands.CreateVacancy;
using JobFinder.Application.Features.Vacancies.Queries;
using JobFinder.Application.Features.Vacancies.Queries.GetVacancies;
using JobFinder.Application.Features.Vacancies.Queries.GetVacancyById;
using JobFinder.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.API.Controllers
{
    [Route("job/vacancies")]
    [ApiController]
    public class VacanciesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VacanciesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PaginatedList<VacancyDto>>> GetFiltered([FromQuery] GetVacanciesFilteredQuery query)
        {
            var vacancies = await _mediator.Send(query);
            return Ok(vacancies);
        }

        [HttpPost("create")]
        [Authorize(Policy = "EmployerOnly")]
        public async Task<ActionResult<int>> Create([FromBody] CreateVacancyCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = id }, id);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<VacancyDto>> GetById(GetVacancyByIdQuery query)
        {
            var vacancy = await _mediator.Send(query);
            return Ok(vacancy);
        }
    }
}
