using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Services;

public class TodoContext : DbContext
{
    public DbSet<Tag> Tags => Set<Tag>();

    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }
}