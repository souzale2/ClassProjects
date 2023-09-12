

using Microsoft.EntityFrameworkCore;
 
namespace GuitarShop.Models
{
    public class ContextCoordinates:DbContext
    {

        public ContextCoordinates(DbContextOptions<ContextCoordinates> options)
            :base(options) { }
        

        public DbSet<CellCoordinates> CellCoordinates { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CellCoordinates>().HasData(
                    new CellCoordinates 
                    { 
                        ID = "11",
                        color = "blue"
                    },
                    new CellCoordinates
                    {
                        ID = "12",
                        color = "blue"
                    },
                    new CellCoordinates
                    {
                        ID = "13",
                        color = "blue"
                    },
                    new CellCoordinates
                    {
                        ID = "21",
                        color = "blue"
                    },
                    new CellCoordinates
                    {
                        ID = "22",
                        color = "blue"
                    },
                    new CellCoordinates
                    {
                        ID = "23",
                        color = "blue"
                    },
                    new CellCoordinates
                    {
                        ID = "31",
                        color = "blue"
                    },
                    new CellCoordinates
                    {
                        ID = "32",
                        color = "blue"
                    },
                    new CellCoordinates
                    {
                        ID = "33",
                        color = "blue"
                    }
                );
        }

       
    }
}
