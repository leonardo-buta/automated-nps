using AutoMapper;

namespace NPS.Messages.Dto
{
    public class MessageMapProfile : Profile
    {
        public MessageMapProfile()
        {
            CreateMap<Message, MessageDto>().ForMember(x => x.MessageType, opt => opt.MapFrom(x => x.MessageType.Type))
                                            .ForMember(x => x.Campaign, opt => opt.MapFrom(x => x.Campaign.Name));
        }
    }
}
