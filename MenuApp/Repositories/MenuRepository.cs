using MenuApp.Data;
using MenuApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MenuApp.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly DataContext _context;
        public MenuRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public void AddMenu(MenuDto _menu)
        {
            Menu menu = new Menu
            {
                Name = _menu.Name,
                CategoryId= _menu.CategoryId,
                ImgPath = _menu.ImgPath,
                Price = _menu.Price,
            };
            _context.Menus.Add(menu);
            _context.SaveChanges();
        }

        public void DeleteMenu(MenuDto _menu)
        {
            var existingMenu = _context.Categories.FirstOrDefault(c => c.Id == _menu.Id);
            if (existingMenu != null)
            {
                _context.Categories.Remove(existingMenu);
                _context.SaveChanges();
            }
        }

        public List<MenuDto> GetAllMenu()
        {
            List<Menu> menus = _context.Menus.ToList();

            var menuDTO = new List<MenuDto>();
            foreach (var _menu in menus)
            {
                menuDTO.Add(new MenuDto
                {
                    Id = _menu.Id,
                    Name = _menu.Name,  
                    CategoryId = _menu.CategoryId,
                    ImgPath = _menu.ImgPath,
                    Price = _menu.Price,
                });
            }

            return menuDTO;
        }

        public MenuDto GetMenuById(int id)
        {
            var menu = _context.Menus.FirstOrDefault(c => c.Id == id);
            if (menu == null) return null;

            return new MenuDto
            {
                Id = menu.Id,
                Name = menu.Name,
                CategoryId = menu.CategoryId,
                ImgPath = menu.ImgPath,
                Price = menu.Price,
            };
        }

        public void UpdateMenu(MenuDto _menu)
        {
            var existingMenu = _context.Menus.FirstOrDefault(c => c.Id == _menu.Id);

            if (existingMenu != null)
            {
                existingMenu.Id = _menu.Id;
                existingMenu.Name = _menu.Name;
                existingMenu.CategoryId = _menu.CategoryId;
                existingMenu.ImgPath = _menu.ImgPath;
                existingMenu.Price = _menu.Price;
                _context.SaveChanges();
            }
        }
    }
}
