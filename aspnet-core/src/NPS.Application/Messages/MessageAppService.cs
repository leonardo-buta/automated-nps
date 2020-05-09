using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using NPS.Messages.Dto;
using NPS.MessageTypes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NPS.Messages
{
    public class MessageAppService : AsyncCrudAppService<Message, MessageDto>, IMessageAppService
    {
        private readonly IRepository<MessageType> _messageTypeRepository;

        public MessageAppService(IRepository<Message> repository, IRepository<MessageType> messageTypeRepository)
            : base(repository)
        {
            _messageTypeRepository = messageTypeRepository;
        }

        public override async Task<MessageDto> CreateAsync(MessageDto input)
        {
            var message = ObjectMapper.Map<Message>(input);

            message.MessageType = await _messageTypeRepository.GetAsync(input.MessageTypeId);

            await Repository.InsertAsync(message);

            return MapToEntityDto(message);
        }

        public override async Task<PagedResultDto<MessageDto>> GetAllAsync(PagedAndSortedResultRequestDto input)
        {
            var results = ObjectMapper.Map<List<MessageDto>>(await Repository.GetAllIncluding(x => x.MessageType).ToListAsync());
            var count = await Repository.CountAsync();

            return new PagedResultDto<MessageDto>(count, results);
        }
    }
}
