using Abp.Application.Services;
using NPS.Messages.Dto;

namespace NPS.Messages
{
    public interface IMessageAppService : IAsyncCrudAppService<MessageDto, int, GetAllMessageInput, CreateMessageInput, UpdateMessageInput>
    {
        
    }
}
