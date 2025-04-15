using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.TagDtos;
using Api.Models;

namespace Api.Mappers
{
    public static class TagMappers
    {
        public static TagDto ToTagDto(this Tag tagModel)
        {
            return new TagDto
            {
                Id = tagModel.Id,
                Name = tagModel.Name
            };
        }
        public static Tag ToTagFromRequestDto(this CreateTagRequestDto tagDto)
        {
            return new Tag
            {
                Name = tagDto.Name,
            };
        }
        
    }
}