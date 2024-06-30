using Microsoft.EntityFrameworkCore;

namespace MenuApp.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        {
        }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
