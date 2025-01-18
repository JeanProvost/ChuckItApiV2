using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckIt.Core.Entities.Base
{
    public class BaseEntity<TId>
    {
        public BaseEntity()
        {
            Id = default!;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;

        }

        [Key]
        public TId Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
