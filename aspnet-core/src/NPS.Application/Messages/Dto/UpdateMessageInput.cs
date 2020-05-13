using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace NPS.Messages.Dto
{
    [AutoMap(typeof(Message))]
    public class UpdateMessageInput : IEntityDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Text { get; set; }

        [Required]
        public int MessageTypeId { get; set; }

        [Required]
        public int CampaingId { get; set; }
    }
}
