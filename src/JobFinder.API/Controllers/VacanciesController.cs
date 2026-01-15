using JobFinder.Application.Features.Vacancies.Commands.CreateVacancy;
using JobFinder.Application.Features.Vacancies.Queries.GetVacancies;
using JobFinder.Application.Models;
using MediatR;
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
        public async Task<ActionResult<PaginatedList<VacancyDto>>> GetFiltered([FromQuery] GetVacanciesFilteredQuery query)
        {
            var vacancies = await _mediator.Send(query);
            return Ok(vacancies);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateVacancyCommand command)
        {
            var id = await _mediator.Send(command);
            return Created("", id);
        }
    }
}
