using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskBoard.Domain.Enum;
using TaskBoard.Application.Interfaces;

namespace TaskBoard.Application.Tasks.Commands.UpdateTasks
{
    public record UpdateTaskStatusCommand(Guid Id, TaskToDoStatus NewStatus) : IRequest<bool>;

    public class UpdateTaskStatusCommandHandler : IRequestHandler<UpdateTaskStatusCommand, bool>
    {
        private readonly IAppDbContext _context;

        public UpdateTaskStatusCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tasks.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null)
                return false;

            try
            {
                switch (request.NewStatus)
                {
                    case TaskToDoStatus.Completed:
                        entity.MarkComplete();
                        break;

                    case TaskToDoStatus.InProgress:
                        entity.MarkInProgress();
                        break;

                    case TaskToDoStatus.Todo:
                        entity.ReopenTask();
                        break;
                }

                _context.Tasks.Update(entity);

                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (InvalidOperationException ex)
            {
                return false;
            }
        }
    }

}
