using DotNetCoreWebAPI_Practice_01.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreWebAPI_Practice_01.Data
{
    public class ContactAPIDbContext : DbContext
    {
        public ContactAPIDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Contact>  Contacts { get; set; }
    }
}
