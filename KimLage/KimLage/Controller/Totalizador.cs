using System;
using LINQtoCSV;
using System.Linq;
using KimLage.Data;
using KimLage.Tools;
using System.Diagnostics;

namespace KimLage.Controller
{
    public class Totalizador
    {
        public void Executar()
        {
            using (LoadFileCSV loadFileCSV = new LoadFileCSV())
            {
                var mainExecutation = Stopwatch.StartNew();
                try
                {
                    var listaOperacoes = loadFileCSV.CarregarArquivo<Operacao>(System.Configuration.ConfigurationManager.AppSettings["arquivoOperacoes"]);
                    var listaMercado = loadFileCSV.CarregarArquivo<Mercado>(System.Configuration.ConfigurationManager.AppSettings["arquivoDadosMercado"]);
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
                    loadFileCSV.Save(resul);
                }
                catch (LINQtoCSVException ex)
                {
                    Console.WriteLine("Erro ao executar programa -> {0}", ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao executar programa -> {0}", ex.Message);
                }
                finally
                {
                    mainExecutation.Stop();
                    Console.WriteLine("Tempo total da execução {0}", mainExecutation.Elapsed);
                }
                Console.ReadLine();
            }
        }
    }
}
