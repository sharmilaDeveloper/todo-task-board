using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskBoard.Application.Interfaces;

namespace TaskBoard.Application.Tasks.Commands.DeleteTasks
{
    public record DeleteTaskCommand(Guid Id) : IRequest<bool>;

    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
    {
        private readonly IAppDbContext _context;

        public DeleteTaskCommandHandler(IAppDbContext context) => _context = context;

        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tasks.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
                return false;

            _context.Tasks.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
