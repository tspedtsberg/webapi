using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.TagDtos;
using Api.Models;

namespace Api.Interfaces
{
    public interface ITagsRepo
    {
        Task<List<Tag>> GetAllAsync();
        Task<Tag?> GetByNameAsync(string name);
        Task<Tag> CreateAsync(Tag tag);
    }
}