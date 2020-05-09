using Abp.Domain.Entities;
using System;

namespace NPS.Campanhas
{
    public class UploadArquivo : Entity
    {
        public string Nome { get; set; }

        public int Tamanho { get; set; }

        public bool EmProcessamento { get; set; }

        public bool Processado { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}
