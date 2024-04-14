using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Interfaces;
public interface ITodoContext
{
    DbSet<Tag> Tags { get;  }
    // Other members if needed
    public int SaveChanges();
}
