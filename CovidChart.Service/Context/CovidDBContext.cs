
using CovidChart.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace CovidChart.Services.Context
{
    public class CovidDBContext : DbContext
    {
        public CovidDBContext(DbContextOptions<CovidDBContext> options) : base(options)
        {
            
        }
        public DbSet<Covid> Covids { get; set; }
    }
}