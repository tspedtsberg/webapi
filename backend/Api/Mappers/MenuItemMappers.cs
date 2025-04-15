using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.MenuItemDtos;
using Api.Models;

namespace Api.Mappers
{
    public static class MenuItemMappers
    {
        public static MenuItemDto ToMenuItemDto(this MenuItem menuItemModel)
        {
            return new MenuItemDto
            {
                Id = menuItemModel.Id,
                Name = menuItemModel.Name,
                Price = menuItemModel.Price,
                Category = menuItemModel.Category,
                Description = menuItemModel.Description ?? string.Empty,
                Tags = menuItemModel.MenuItemTags.Select(mt => mt.Tag.Name).ToList()
                
            };
        }

        public static MenuItem ToMenuItemFromRequestDto(this CreateMenuItemRequestDto menuItemDto)
        {
            return new MenuItem
            {
                Name = menuItemDto.Name,
                Price = menuItemDto.Price,
                Category = menuItemDto.Category,
                Description = menuItemDto.Description
            };

        }
    }
}