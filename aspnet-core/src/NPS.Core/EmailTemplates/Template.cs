using Abp.Domain.Entities;

namespace NPS.EmailTemplates
{
    public class Template : Entity
    {
        public string Body { get; set; }

        public bool Active { get; set; }
    }
}
