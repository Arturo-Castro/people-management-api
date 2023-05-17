using Microsoft.EntityFrameworkCore;
using PeopleManagerApp.Domain.Entities;

namespace PeopleManagerApp.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<Person> People { get; set; }
    }
}