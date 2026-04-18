using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskBoard.Application.Interfaces;
using TaskBoard.Application.Tasks.Queries.GetTasks;

namespace TaskBoard.Application.Tasks.Commands.ReopenTasks
{
    public record ReopenTaskCommand(Guid Id) : IRequest<bool>;
    public class ReopenTaskCommandHandler : IRequestHandler<ReopenTaskCommand, bool>
    {
        private readonly IAppDbContext _context;

        public ReopenTaskCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ReopenTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks.FindAsync(request.Id);

            if (task == null)
                return false;

            task.ReopenTask();

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
