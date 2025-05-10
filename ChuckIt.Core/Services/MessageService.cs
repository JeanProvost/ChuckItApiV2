using ChuckIt.Core.Interfaces.IRepositories;
using ChuckIt.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckIt.Core.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageRepository(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
    }
}
