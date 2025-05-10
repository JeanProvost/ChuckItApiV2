using ChuckItApiV2.Core.Entities.Messages;
using ChuckItApiV2.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckIt.Infrastructure.Repositories
{
    public class MessageRepository : BaseRepository<Message>
    {
        public MessageRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
