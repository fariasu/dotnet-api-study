using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.DataAccess.Db;

public class TaskManagerDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<TaskEntity> Tasks { get; set; }
}