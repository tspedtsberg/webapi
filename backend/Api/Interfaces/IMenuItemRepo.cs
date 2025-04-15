using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.MenuItemDtos;
using Api.Helper;
using Api.Models;

namespace Api.Interfaces
{
    public interface IMenuItemRepo
    {
        Task<List<MenuItem>> GetAllAsync(QueryObject query);
        Task<MenuItem> GetByIdAsync(int id);
        Task<MenuItem> CreateAsync(MenuItem menuItem);
        Task<MenuItem?> UpdateAsync(int id, UpdateMenuItemRequestDto updateDto);
        Task<MenuItem> DeleteAsync(int id);
    }
}