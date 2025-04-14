using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Common
{
    public class LeitorDeBoleto
    {
        public List<Boleto> LerBoletos(string caminhoArquivo)
        {
            // montar lista de boletos
            var boletos = new List<Boleto>();

            // ler arquivo de boletos
            using (var reader = new StreamReader(caminhoArquivo))
            {
                // ler cabeçalho do arquivo CSV
                string? linha = reader.ReadLine();
                if (linha == null)
                    throw new Exception("Arquivo inválido");

                string[] cabecalho = linha.Split(',');

                // para cada linha do arquivo CSV
                while (!reader.EndOfStream)
                {
                    // ler dados
                    linha = reader.ReadLine();
                    if (linha == null)
                        throw new Exception("Arquivo inválido");

                    string[] dados = linha.Split(',');

                    // carregar objeto Boleto
                    Boleto boleto = MapearTextoParaObjeto<Boleto>(cabecalho, dados);

                    // adicionar boleto à lista
                    boletos.Add(boleto);
                }
            }

            // retornar lista de boletos
            return boletos;
        }

        private T MapearTextoParaObjeto<T>(string[] nomesPropriedades, string[] valoresPropriedades)
        {
            T instancia = Activator.CreateInstance<T>();
            if (instancia == null)
                throw new Exception("Erro na criação da instância do objeto");

            for (int i = 0; i < nomesPropriedades.Length; i++)
            {
                string nomePropriedade = nomesPropriedades[i];
                PropertyInfo? propertyInfo = instancia.GetType().GetProperty(nomePropriedade);

                if (propertyInfo != null)
                {
                    Type propertyType = propertyInfo.PropertyType;
                    string valor = valoresPropriedades[i];

                    object valorConvertido = Convert.ChangeType(valor, propertyType);

                    propertyInfo.SetValue(instancia, valorConvertido);
                }
            }

            return instancia;
        }
    }
}
