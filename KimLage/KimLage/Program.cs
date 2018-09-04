using System;
using System.Collections.Generic;
using System.Linq;
using KimLage.Data;
using KimLage.Tools;
using System.Diagnostics;

namespace KimLage
{
    class Program
    {
        static void Main(string[] args)
        {
            using (LoadFileCSV loadFileCSV = new LoadFileCSV())
            {
                var mainExecutation = Stopwatch.StartNew();
                var listaOperacoes = loadFileCSV.CarregarArquivo<Operacao>(@"../../../../ProvaCSharp/Operacoes.csv");//TODO AJUSTAR O CAMINHO
                var listaMercado = loadFileCSV.CarregarArquivo<Mercado>(@"../../../../ProvaCSharp/DadosMercado.csv");//TODO AJUSTAR O CAMINHO
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
                             select new Resultado
                             {
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
}
