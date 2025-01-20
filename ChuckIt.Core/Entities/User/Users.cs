using ChuckItApiV2.Core.Entities.Base;
using ChuckItApiV2.Core.Entities.Listing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckItApiV2.Core.Entities.User
{
    public class Users : BaseEntity<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public ICollection<Listings> Listings { get; set; } = new List<Listings>();
    }
}
