
using EDI.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace EDI.DataAccess.Context
{
    public class CosmosContext : DbContext
    {

        public CosmosContext(DbContextOptions<CosmosContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(
                "https://b3e0bfa5-0ee0-4-231-b9ee.documents.azure.com:443/",
                "wwkz0XbBDa4FgWayuFw4rNGDVwm3phIVde0QyBvcDsPezpZNTbrkZ82hUntH3IzMu40x1kt56ZWLRQj2m6DWdg==",
                "advent-final"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemContainer>()
                .ToContainer("Item")
                .HasOne(item => item.ContainerId);
        }
    }
}
