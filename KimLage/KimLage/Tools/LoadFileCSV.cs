using System;
using LINQtoCSV;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace KimLage.Tools
{   
    public class LoadFileCSV : IDisposable
    {
        private CsvFileDescription inputFileDescription = null;

        public LoadFileCSV()
        {
            inputFileDescription = new CsvFileDescription
            {
                SeparatorChar = Convert.ToChar(System.Configuration.ConfigurationManager.AppSettings["separadorCampos"]),
                FirstLineHasColumnNames = true,
                IgnoreUnknownColumns = true
            };
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                inputFileDescription = null;
            }
        }

        public IList<T> CarregarArquivo<T>(string arquivo) where T : class, new()
        {
            var intermediario = Stopwatch.StartNew();
            var list = new CsvContext().Read<T>(arquivo, inputFileDescription).ToList();
            intermediario.Stop();
            Console.WriteLine("Total da leitura das {2} {0} para {1} registro(s)", intermediario.Elapsed, list.Count, typeof(T).ToString());
            return list;
        }

        public void Save<T>(IList<T> lista) where T : class, new()
        {
            new CsvContext().Write(lista, System.Configuration.ConfigurationManager.AppSettings["arquivoFinal"], inputFileDescription);
        }
    }
}
