using Abp.Domain.Entities;
using System;

namespace NPS.Campaings
{
    public class Campaing : AuditedEntity
    {
        public string Name { get; set; }

        public string Descricao { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public DateTime DataCriacao { get; set; }

        public bool Ativo { get; set; }
    }
}
