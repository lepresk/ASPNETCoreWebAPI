namespace RestApi;

using Microsoft.EntityFrameworkCore;
using RestApi.Models;

public class ModelContext : DbContext
{
    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {

    }

    public DbSet<Post> Posts { get; set; }
}
