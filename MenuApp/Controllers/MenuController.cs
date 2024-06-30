using MenuApp.Data;
using MenuApp.Models;
using MenuApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MenuApp.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuRepository _menuRepository;
        private readonly ICategoryRepositorycs _categoryRepository;

        public MenuController(IMenuRepository menuRepository, ICategoryRepositorycs categoryRepository)
        {
            _menuRepository = menuRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult AddMenu()
        {
            ViewBag.Categories = _categoryRepository.GetAllCategories();

            return View();
        }

        [HttpPost]
        public IActionResult AddMenu(MenuDto  menuDto)
        {
            if (ModelState.IsValid)
            {
                if (menuDto.File != null && menuDto.File.Length > 0)
                {
                    string extension = Path.GetExtension(menuDto.File.FileName);
                    string filename = Guid.NewGuid().ToString() + extension;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", filename);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        menuDto.File.CopyTo(stream);
                    }
                    menuDto.ImgPath = "/img/" + filename;
                }
                else
                {
                    ViewBag.Message = "Lütfen bir dosya seçin.";
                    return View(menuDto);
                }

                menuDto.Category = _categoryRepository.GetCategoryById(menuDto.CategoryId);
                _menuRepository.AddMenu(menuDto);
                return RedirectToAction("MenuList");
            }
            return View(menuDto);
        }
        public IActionResult MenuList()
        {
            ViewBag.Categories = _categoryRepository.GetAllCategories();
            var menus = _menuRepository.GetAllMenu();
            return View(menus);
        }

        [HttpGet]
        public IActionResult UpdateMenu(int id)
        {
            ViewBag.Categories = _categoryRepository.GetAllCategories();
            var menu = _menuRepository.GetMenuById(id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        [HttpPost]
        public IActionResult UpdateMenu(MenuDto menuDto)
        {
            if (ModelState.IsValid)
            {
                var existingMenu = _menuRepository.GetMenuById(menuDto.Id);
                if (existingMenu == null)
                {
                    return NotFound();
                }

                if (menuDto.File != null && menuDto.File.Length > 0)
                {
                    // Eski dosyayı silme
                    if (!string.IsNullOrEmpty(existingMenu.ImgPath))
                    {
                        var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingMenu.ImgPath.TrimStart('/'));
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }

                    // Yeni dosyayı kaydetme
                    var fileExtension = Path.GetExtension(menuDto.File.FileName);
                    string fileName = Guid.NewGuid().ToString() + fileExtension;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        menuDto.File.CopyTo(stream); // .Wait() yerine doğrudan senkron CopyTo kullanıyoruz
                    }
                    menuDto.ImgPath = "/img/" + fileName;
                }
                else
                {
                    menuDto.ImgPath = existingMenu.ImgPath; // Mevcut resmi koruyoruz
                }

                

                _menuRepository.UpdateMenu(menuDto);
                return RedirectToAction("MenuList");
            }
            return View(menuDto);
        }

        [HttpGet]
        public IActionResult DeleteMenu(int id)
        {

            var menu = _menuRepository.GetMenuById(id);
            if (menu == null)
            {
                return NotFound();
            }
            _menuRepository.DeleteMenu(menu);
            return RedirectToAction("MenuList");
        }
    }
}
