using MenuApp.Models;

namespace MenuApp.Repositories
{
    public interface IMenuRepository
    {
        void AddMenu(MenuDto _menu);
        void UpdateMenu(MenuDto _menu);
        void DeleteMenu(MenuDto _menu);
        List<MenuDto> GetAllMenu();
        MenuDto GetMenuById(int id);

    }
}
