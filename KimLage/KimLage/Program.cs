using System;
using System.Collections.Generic;
using System.Linq;
using KimLage.Data;
using LINQtoCSV;
using System.Diagnostics;

namespace KimLage
{
    class Program
    {
        //https://www.codeproject.com/Articles/25133/LINQ-to-CSV-library

        private static IList<T> CarregarArquivo<T>(string arquivo, CsvFileDescription inputFileDescription) where T : class, new()
        {
            var intermediario = Stopwatch.StartNew();
            var list = new CsvContext().Read<T>(arquivo, inputFileDescription).ToList();
            intermediario.Stop();
            Console.WriteLine("Total da leitura das {2} {0} para {1} registro(s)", intermediario.Elapsed, list.Count, typeof(T).ToString());
            return list;
        }

        static void Main(string[] args)
        {
            var mainExecutation = Stopwatch.StartNew();

            CsvFileDescription inputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ';',//TODO - CONFIGURAR SEPARADOR
                FirstLineHasColumnNames = true,
                IgnoreUnknownColumns = true
            };


            var listaOperacoes = CarregarArquivo<Operacao>(@"../../../../ProvaCSharp/Operacoes.csv", inputFileDescription);//TODO AJUSTAR O CAMINHO
            var listaMercado = CarregarArquivo<Mercado>(@"../../../../ProvaCSharp/DadosMercado.csv", inputFileDescription);//TODO AJUSTAR O CAMINHO

            var resul = (from operacao in listaOperacoes
                         join mercado in listaMercado
                         on new { operacao.IdPreco, operacao.NumeroPrazoDiasCorrigos } equals new { mercado.IdPreco, mercado.NumeroPrazoDiasCorrigos }
                         into unionMercado
                         from temMercado in unionMercado.DefaultIfEmpty()
                         group new
                         {
                             operacao,
                             total = operacao.Quantidade * (temMercado != null ? temMercado.ValorPreco : 0)
                         }
                         by new
                         {
                             operacao.NomeSubProduto,
                         }
                         into grupo
                         select new Resultado {
                             NomeSubProduto = grupo.Key.NomeSubProduto,
                             Total = grupo.Sum(t => t.total)
                         }).ToList();

            Console.WriteLine("Total de registro {0}", resul.Count);
            resul.ForEach(o => Console.WriteLine("NomeSubProduto = {0} , Total = {1}", o.NomeSubProduto, o.Total));


            mainExecutation.Stop();
            Console.WriteLine(mainExecutation.Elapsed);
            Console.ReadLine();
        }



    }
}
