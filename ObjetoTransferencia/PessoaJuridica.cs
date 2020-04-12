using System;

namespace ObjetoTransferencia
{
    public class PessoaJuridica
    {
        public Pessoa Pessoa { get; set; }
        public string NomeFantasia { get; set; }

        public string RazaoSocial { get; set; }

        public string CNPJ { get; set; }

        public DateTime DataFundacao { get; set; }
    }
}
