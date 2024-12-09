using Microsoft.EntityFrameworkCore;
using Test_Popsy.Models;

namespace Test_Popsy.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<TaskModel> TaskModel { get; set; }
    }
}
