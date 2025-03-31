using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Common
{
    internal class BoletosPorCedente
    {
        [NomeColuna("Cedente Nome")]
        public string? CedenteNome { get; set; }

        [NomeColuna("Cedente CPF/CNPJ")]
        public string? CedenteCpfCnpj { get; set; }

        [NomeColuna("Cedente Agencia")]
        public string? CedenteAgencia { get; set; }

        [NomeColuna("Cedente Conta")]
        public string? CedenteConta { get; set; }

        [NomeColuna("Total")]
        public decimal Valor { get; set; }

        [NomeColuna("Qtd")]
        public int Quantidade { get; set; }
    }
}
