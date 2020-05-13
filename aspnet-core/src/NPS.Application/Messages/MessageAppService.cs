using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using NPS.Campaings;
using NPS.Messages.Dto;
using NPS.MessageTypes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NPS.Messages
{
    public class MessageAppService : AsyncCrudAppService<Message, MessageDto, int, GetAllMessageInput, CreateMessageInput, UpdateMessageInput>, IMessageAppService
    {
        private readonly IRepository<MessageType> _messageTypeRepository;
        private readonly IRepository<Campaign> _campaingRepository;

        public MessageAppService(IRepository<Message> repository, IRepository<MessageType> messageTypeRepository, IRepository<Campaign> campaingRepository)
            : base(repository)
        {
            _messageTypeRepository = messageTypeRepository;
            _campaingRepository = campaingRepository;
        }

        public override async Task<MessageDto> CreateAsync(CreateMessageInput input)
        {
            var message = ObjectMapper.Map<Message>(input);

            message.MessageType = await _messageTypeRepository.GetAsync(input.MessageTypeId);
            message.Campaing = await _campaingRepository.GetAsync(input.CampaingId);

            await Repository.InsertAsync(message);

            return MapToEntityDto(message);
        }

        public override async Task<MessageDto> UpdateAsync(UpdateMessageInput input)
        {
            var message = await GetEntityByIdAsync(input.Id);

            ObjectMapper.Map(input, message);

            message.MessageType = await _messageTypeRepository.GetAsync(input.MessageTypeId);
            message.Campaing = await _campaingRepository.GetAsync(input.CampaingId);

            await Repository.UpdateAsync(message);

            return MapToEntityDto(message);
        }

        public override async Task<PagedResultDto<MessageDto>> GetAllAsync(GetAllMessageInput input)
        {
            var results = ObjectMapper.Map<List<MessageDto>>(await Repository.GetAllIncluding(x => x.MessageType).ToListAsync());
            var count = await Repository.CountAsync();

            return new PagedResultDto<MessageDto>(count, results);
        }
    }
}
