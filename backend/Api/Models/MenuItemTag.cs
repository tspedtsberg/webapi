using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    [Table("MenuItemTags")]
    public class MenuItemTag
    {
        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; } = null;

        public int TagId { get; set; }
        public Tag Tag { get; set; } = null;
    }
}