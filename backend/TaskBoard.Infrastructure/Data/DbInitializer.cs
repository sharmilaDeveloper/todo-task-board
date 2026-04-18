using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskBoard.Domain.Entities;

namespace TaskBoard.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (await context.Tasks.AnyAsync())
                return;

            var tasks = new List<ToDoTaskItem>
            {
            new ToDoTaskItem("Shopping", "Go to grace super market to buy some vegetables"),
            new ToDoTaskItem("Read Book", "Read the story book from page 50 to 100"),
            new ToDoTaskItem("Clean the house", "Clean the room , hall , balcony and stair case")
        };

            tasks[0].MarkComplete(); 
            tasks[1].MarkInProgress();

            await context.Tasks.AddRangeAsync(tasks);
            await context.SaveChangesAsync();
        }
    }
}
