using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Dtos.MenuItemDtos;
using Api.Helper;
using Api.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository
{
    public class MenuItemRepository : IMenuItemRepo
    {
        private readonly ApplicationDBContext _context;
        public MenuItemRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<MenuItem> CreateAsync(MenuItem menuItem)
        {
            await _context.MenuItems.AddAsync(menuItem);
            await _context.SaveChangesAsync();
            return menuItem;
        }

        public async Task<MenuItem> DeleteAsync(int id)
        {
            var menuItemModel = await _context.MenuItems.FirstOrDefaultAsync(x => x.Id == id);

            if (menuItemModel == null)
            {
                return null;
            }
            _context.MenuItems.Remove(menuItemModel);
            await _context.SaveChangesAsync();
            return menuItemModel;
        }

        public async Task<List<MenuItem>> GetAllAsync(QueryObject query)
        {
            var menuItems = _context.MenuItems.Include(m => m.MenuItemTags).ThenInclude(mt => mt.Tag).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Tag))
            {
                var tagList = query.Tag.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim().ToLower());

                menuItems = menuItems.Where(m => m.MenuItemTags.Any(mt => tagList.Contains(mt.Tag.Name.ToLower())));
            }

            return await menuItems.ToListAsync();
        }

        public async Task<MenuItem> GetByIdAsync(int id)
        {
            return await _context.MenuItems.FindAsync(id);
        }

        public async Task<MenuItem?> UpdateAsync(int id, UpdateMenuItemRequestDto updateDto)
        {
            var existingMenuItem = await _context.MenuItems.FirstOrDefaultAsync(x => x.Id == id);

            if (existingMenuItem == null)
            {
                return null;
            }

            existingMenuItem.Name = updateDto.Name;
            existingMenuItem.Price = updateDto.Price;
            existingMenuItem.Category = updateDto.Category;
            existingMenuItem.Description = updateDto.Description;

            await _context.SaveChangesAsync();
            return existingMenuItem;
        }
    }
}