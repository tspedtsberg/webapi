using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Dtos.TagDtos;
using Api.Interfaces;
using Api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ITagsRepo _tagsRepo;
        public TagController(ApplicationDBContext context, ITagsRepo tagsrepo)
        {
            _context = context;
            _tagsRepo = tagsrepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _tagsRepo.GetAllAsync();

            var tagDtos = tags.Select(s => s.ToTagDto()).ToList();

            return Ok(tagDtos);
        }
        
        [HttpGet]
        [Route("{name:alpha}")]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            var tag = await _tagsRepo.GetByNameAsync(name);

            if(tag == null)
            {
                return NotFound();
            }
            return Ok(tag.ToTagDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTagRequestDto tagRequestDto)
        {
            var tagModel = tagRequestDto.ToTagFromRequestDto();
            await _tagsRepo.CreateAsync(tagModel);
            return CreatedAtAction(nameof(Create), new { id = tagModel.Id }, tagModel);
        }
        
        //post(set tags)
        //delete
        
    }
}