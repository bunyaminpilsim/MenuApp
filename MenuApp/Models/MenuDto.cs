namespace MenuApp.Models
{
    public class MenuDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? ImgPath { get; set; }
        public IFormFile? File { get; set; }
        public CategoryDto? Category { get; set; }
        public int CategoryId { get; set; }
    }
}
