﻿using System.Reflection;

namespace ByteBank.Common
{
    public class RelatorioDeBoletoCSV : IRelatorio<Boleto>
    {
        private readonly string nomeArquivoSaida;
        private readonly DateTime dataRelatorio;

        public RelatorioDeBoletoCSV(string nomeArquivoSaida, DateTime dataRelatorio)
        {
            this.nomeArquivoSaida = nomeArquivoSaida;
            this.dataRelatorio = dataRelatorio;
        }

        public RelatorioDeBoletoCSV(DateTime dataRelatorio)
        {
            this.dataRelatorio = dataRelatorio;
            this.nomeArquivoSaida = "RelatorioDeBoleto.csv";
        }

        public RelatorioDeBoletoCSV(string nomeArquivoSaida)
        {
            this.nomeArquivoSaida = nomeArquivoSaida;
            this.dataRelatorio = DateTime.Now;
        }

        public void Processar(List<string> lista)
        {
            Console.WriteLine($"--------{this.dataRelatorio}--------");

            Console.WriteLine(string.Join("\n", lista));
        }

        public void Processar(List<Boleto> boletos)
        {
            Console.WriteLine($"--------{ this.dataRelatorio }--------");

            var boletosPorCedente = PegaBoletosAgrupados(boletos);

            GravarArquivo(boletosPorCedente);
        }

        private void GravarArquivo(List<BoletosPorCedente> grupos)
        {
            // Obter tipo da classe
            Type tipo = typeof(BoletosPorCedente);


            // Obter atributo da classe para titulo do relatório
            var titulo = tipo.GetCustomAttributes<NomeColunaAttribute>().FirstOrDefault()?.Header ?? tipo.Name;

            Console.WriteLine("-------------");
            Console.WriteLine(titulo);
            Console.WriteLine("-------------");

            // Usar Reflection para obter propriedades
            PropertyInfo[] propriedades = tipo.GetProperties();

            // Escrever os dados no arquivo CSV
            using (var sw = new StreamWriter(nomeArquivoSaida))
            {
                // Escrever cabeçalho
                var cabecalho = propriedades
                    .Select(p => p.GetCustomAttributes<NomeColunaAttribute>().FirstOrDefault()?.Header ?? p.Name);

                sw.WriteLine(string.Join(',', cabecalho));

                // Escrever linhas do relatório
                foreach (var grupo in grupos)
                {
                    var valores = propriedades.Select(p => p.GetValue(grupo));
                    sw.WriteLine(string.Join(',', valores));
                }
            }

            Console.WriteLine($"Arquivo '{nomeArquivoSaida}' criado com sucesso!");
        }

        private List<BoletosPorCedente> PegaBoletosAgrupados(List<Boleto> boletos)
        {
            // Agrupar boletos por cedente
            var boletosAgrupados = boletos.GroupBy(b => new
            {
                b.CedenteNome,
                b.CedenteCpfCnpj,
                b.CedenteAgencia,
                b.CedenteConta
            });

            // Lista para armazenar instâncias de BoletosPorCedente
            List<BoletosPorCedente> boletosPorCedenteList = new List<BoletosPorCedente>();

            // Iterar sobre os grupos de boletos por cedente
            foreach (var grupo in boletosAgrupados)
            {
                // Criar instância de BoletosPorCedente
                BoletosPorCedente boletosPorCedente = new BoletosPorCedente
                {
                    CedenteNome = grupo.Key.CedenteNome,
                    CedenteCpfCnpj = grupo.Key.CedenteCpfCnpj,
                    CedenteAgencia = grupo.Key.CedenteAgencia,
                    CedenteConta = grupo.Key.CedenteConta,
                    Valor = grupo.Sum(b => b.Valor),
                    Quantidade = grupo.Count()
                };

                // Adicionar à lista
                boletosPorCedenteList.Add(boletosPorCedente);
            }

            return boletosPorCedenteList;
        }
    }
}