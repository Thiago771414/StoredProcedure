using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class EstoqueMovimentado
    {
        public Filial Filial { get; set; }

        public Produto Produto { get; set; }

        public int Quantidade { get; set; }

        public DateTime DataHora { get; set; }

        public string EntrouSaiu { get; set; }
    }
}
