using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskBoard.Application.Tasks.Queries.GetTasks;
using TaskBoard.Application.Tasks.Commands.CreateTasks;
using TaskBoard.Application.Tasks.Commands.DeleteTasks;
using TaskBoard.Application.Tasks.Commands.UpdateTasks;
using TaskBoard.Application.Tasks.Commands.EditTasks;
using TaskBoard.Application.Tasks.Commands.ReopenTasks;



namespace TaskBoard.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskDto>>> GetTasks()
        {
            return await _mediator.Send(new GetTasksQuery());
        }

        [HttpPost]
        public async Task<ActionResult<TaskDto>> Create(CreateTasksCommand command)
        => Ok(await _mediator.Send(command));

        [HttpPut("{id}/status")]
        public async Task<ActionResult> UpdateStatus(Guid id, [FromBody] UpdateTaskStatusCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID Mismatch");

            var result = await _mediator.Send(command);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteTaskCommand(id));
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(Guid id, EditTaskCommand command)
        {
            if (id != command.Id)
                return BadRequest();
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("{id}/reopen")]
        public async Task<IActionResult> Reopen(Guid id)
        {
            var result = await _mediator.Send(new ReopenTaskCommand(id));

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
