using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Interfaces;

namespace TodoApi.Services;

public class TodoContext : DbContext, ITodoContext
{
    public DbSet<Tag> Tags => Set<Tag>();


    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }
}