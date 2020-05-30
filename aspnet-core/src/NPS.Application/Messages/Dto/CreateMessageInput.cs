using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace NPS.Messages.Dto
{
    [AutoMap(typeof(Message))]
    public class CreateMessageInput
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Text { get; set; }

        [Required]
        public int MessageTypeId { get; set; }

        [Required]
        public int CampaignId { get; set; }
    }
}
