using MenuApp.Models;
using System.ComponentModel.DataAnnotations;

namespace MenuApp.Data
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? ImgPath { get; set; }
        public int CategoryId { get; set; }

    }
}
