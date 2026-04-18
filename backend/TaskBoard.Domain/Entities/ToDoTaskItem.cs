
using System.ComponentModel.DataAnnotations;
using TaskBoard.Domain.Enum;

namespace TaskBoard.Domain.Entities
{
    public class ToDoTaskItem : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        public bool IsCompleted => Status == TaskToDoStatus.Completed;
        public TaskToDoStatus Status { get; set; } = TaskToDoStatus.Todo;

        public ToDoTaskItem(string title, string description)
        {
            Title = title;
            Description = description;
            CreatedAt = DateTime.UtcNow;
            Status = TaskToDoStatus.Todo;
        }

        public void Update(string title, string description)
        {
            Title = title;
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkComplete()
        {
            Status = TaskToDoStatus.Completed;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkInProgress()
        {

            if (this.IsCompleted)
            {
                throw new InvalidOperationException("Completed tasks cannot be moved back to In Progress without being reset.");
            }

            Status = TaskToDoStatus.InProgress;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ReopenTask()
        {
            Status = TaskToDoStatus.Todo;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
