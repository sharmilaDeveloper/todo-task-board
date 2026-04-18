using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskBoard.Application.Interfaces;

namespace TaskBoard.Application.Tasks.Commands.EditTasks
{
    public record EditTaskCommand(Guid Id, string Title, string Description) : IRequest<bool>;

    public class EditTaskCommandHandler : IRequestHandler<EditTaskCommand, bool>
    {
        private readonly IAppDbContext _context;

        public EditTaskCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(EditTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tasks.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null)
                return false;

            entity.Update(request.Title, request.Description);

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
