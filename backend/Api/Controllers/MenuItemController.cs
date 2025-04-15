using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Interfaces;
using Api.Mappers;
using Api.Dtos.MenuItemDtos;
using Api.Dtos.MenuItemTagDtos;
using Microsoft.AspNetCore.Mvc;
using Api.Helper;

namespace Api.Controllers
{
    [Route("api/menuitem")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMenuItemRepo _menuItemRepo;
        public MenuItemController(ApplicationDBContext context, IMenuItemRepo menuItemRepo)
        {
            _context = context;
            _menuItemRepo = menuItemRepo;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var menuItems = await _menuItemRepo.GetAllAsync(query);

            var menuItemDto = menuItems.Select(s => s.ToMenuItemDto()).ToList();

            return Ok(menuItemDto);

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var menuItems = await _menuItemRepo.GetByIdAsync(id);
            if (menuItems == null)
            {
                return NotFound();
            }

            return Ok(menuItems.ToMenuItemDto());
            //flere queries
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMenuItemRequestDto createRequestDto)
        {
            var menuItemModel = createRequestDto.ToMenuItemFromRequestDto();
            await _menuItemRepo.CreateAsync(menuItemModel);
            return CreatedAtAction(nameof(Create), new { id = menuItemModel.Id }, menuItemModel.ToMenuItemDto());


        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateMenuItemRequestDto UpdateDto)
        {
            var menuItemModel = await _menuItemRepo.UpdateAsync(id, UpdateDto);
            if (menuItemModel == null)
            {
                return NotFound();
            }

            return Ok(menuItemModel.ToMenuItemDto());
        }
        
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var menuItem = await _menuItemRepo.DeleteAsync(id);

            if (menuItem == null)
            {
                return NotFound();
            }

            return NoContent();
        }
        
        [HttpPost]
        [Route("tag")]
        public async Task<IActionResult> TagMenuItem([FromBody] MenuItemTagDto menuItemTagDto)
        {
            throw new NotImplementedException();
        }

    }
}