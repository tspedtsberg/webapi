using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Dtos.TagDtos;
using Api.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository
{
    public class TagsRepository : ITagsRepo
    {
        private readonly ApplicationDBContext _context;
        public TagsRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Tag> CreateAsync(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
            return tag;

        }

        public async Task<List<Tag>> GetAllAsync()
        {
            var tags = await _context.Tags.ToListAsync();

            return tags;
        }

        public async Task<Tag?> GetByNameAsync(string name)
        {
            return await _context.Tags.FirstOrDefaultAsync(a => a.Name == name);
        }
    }
}