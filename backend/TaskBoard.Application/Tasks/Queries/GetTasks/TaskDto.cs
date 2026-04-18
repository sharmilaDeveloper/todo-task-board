using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBoard.Domain.Enum;

namespace TaskBoard.Application.Tasks.Queries.GetTasks
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskToDoStatus Status { get; set; } = TaskToDoStatus.Todo;

        public bool IsCompleted { get; set; }
    }
}
