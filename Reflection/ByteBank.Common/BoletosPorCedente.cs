using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Common
{
    [NomeColuna("Boletos por Cedente")]
    public class BoletosPorCedente
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

        // Sem atributo para mostrar que o código consegue usar o nome da propriedade quando não encontrar um custom attribute
        public int Quantidade { get; set; }
    }
}
