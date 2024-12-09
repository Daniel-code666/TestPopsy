using Microsoft.EntityFrameworkCore;
using Test_Popsy.Models;

namespace Test_Popsy.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskModel> TaskModel { get; set; }
    }
}
