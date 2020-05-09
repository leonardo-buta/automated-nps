using Abp.Domain.Entities;
using System;

namespace NPS.Campanhas
{
    public class Mensagens : Entity
    {
        public string Nome { get; set; }

        public string Texto { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}
