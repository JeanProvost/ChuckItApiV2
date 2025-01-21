using ChuckItApiV2.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckItApiV2.Core.Entities.Category
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string BackgroundColor { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
    }
}
