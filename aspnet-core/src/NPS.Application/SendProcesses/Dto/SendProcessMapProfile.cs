using AutoMapper;
using NPS.SendProcesses;
using NPS.SendProcesses.Dto;

namespace NPS.Messages.Dto
{
    public class SendProcessMapProfile : Profile
    {
        public SendProcessMapProfile()
        {
            CreateMap<SendProcess, SendProcessDto>().ForMember(x => x.StatusSendProcess, opt => opt.MapFrom(x => x.StatusSendProcess.Name));
        }
    }
}
