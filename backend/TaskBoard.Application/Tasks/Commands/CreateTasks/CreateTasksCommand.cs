using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskBoard.Domain.Enum;
using TaskBoard.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using TaskBoard.Application.Tasks.Queries.GetTasks;
using TaskBoard.Domain.Entities;

namespace TaskBoard.Application.Tasks.Commands.CreateTasks
{
    public record CreateTasksCommand(string Title, string Description) : IRequest<TaskDto>;

    public class CreateTaskCommandHandler : IRequestHandler<CreateTasksCommand, TaskDto>
    {
        private readonly IAppDbContext _context;

        public CreateTaskCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskDto> Handle(CreateTasksCommand request, CancellationToken cancellationToken)
        {
            var entity = new ToDoTaskItem(request.Title, request.Description);

            _context.Tasks.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return new TaskDto {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                IsCompleted = entity.IsCompleted
            };
        }
    }
}
