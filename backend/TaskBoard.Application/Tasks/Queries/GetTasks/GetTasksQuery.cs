using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskBoard.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using TaskBoard.Domain.Enum;

namespace TaskBoard.Application.Tasks.Queries.GetTasks
{
    public record GetTasksQuery : IRequest<List<TaskDto>>;

    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, List<TaskDto>>
    {
        private readonly IAppDbContext _context;

        public GetTasksQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            return await _context.Tasks
                .AsNoTracking() 
                .Select(t => new TaskDto {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status,
                    IsCompleted = t.IsCompleted

                })
                .ToListAsync(cancellationToken);
        }
    }
}
