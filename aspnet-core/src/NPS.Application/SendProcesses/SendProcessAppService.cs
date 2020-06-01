using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using NPS.Messages;
using NPS.SendProcesses.Dto;
using NPS.StatusSendProcesses;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace NPS.SendProcesses
{
    public class SendProcessAppService : AsyncCrudAppService<SendProcess, SendProcessDto, int, GetAllSendProcessInput, CreateSendProcessInput, UpdateSendProcessInput>, ISendProcessAppService
    {
        private readonly IRepository<Message> _messageRepository;
        private readonly IRepository<StatusSendProcess> _statusSendProcessRepository;

        public SendProcessAppService(IRepository<SendProcess> repository, IRepository<Message> messageRepository, IRepository<StatusSendProcess> statusSendProcessRepository)
            : base(repository)
        {
            _messageRepository = messageRepository;
            _statusSendProcessRepository = statusSendProcessRepository;
        }

        public override async Task<PagedResultDto<SendProcessDto>> GetAllAsync(GetAllSendProcessInput input)
        {
            var query = await Repository.GetAllIncluding(x => x.StatusSendProcess)
                                        .Skip(input.SkipCount)
                                        .Take(input.MaxResultCount)
                                        .AsQueryable()
                                        .ToListAsync();

            var count = await Repository.CountAsync();

            return new PagedResultDto<SendProcessDto>(count, ObjectMapper.Map<List<SendProcessDto>>(query));
        }

        public override async Task<SendProcessDto> CreateAsync(CreateSendProcessInput input)
        {
            var process = ObjectMapper.Map<SendProcess>(input);

            process.Message = await _messageRepository.GetAsync(input.MessageId);
            process.StatusSendProcess= await _statusSendProcessRepository.GetAsync(1);

            await Repository.InsertAsync(process);

            return MapToEntityDto(process);
        }
    }
}
