using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskBoard.Domain.Entities;

namespace TaskBoard.Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<ToDoTaskItem> Tasks { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
