using Microsoft.EntityFrameworkCore;

using ToDoList.Models;

namespace ToDoList.Services
{
    public class ApplicationContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();
        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            optionsBuilder.UseSqlite("Data Source=ToDoList.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoItem>().ToTable("todotable");
        }
    }
}
