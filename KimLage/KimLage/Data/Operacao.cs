using System;
using LINQtoCSV;

namespace KimLage.Data
{
    public class Operacao
    {
        [CsvColumn(Name = "CD_OPERACAO", FieldIndex = 1)]
        public string CodigoOperacao { get; set; }

        [CsvColumn(Name = "DT_INICIO", FieldIndex = 2, OutputFormat = "dd/MM/yyyy")]
        public DateTime DataInicio { get; set; }

        [CsvColumn(Name = "DT_FIM", FieldIndex = 3, OutputFormat = "dd/MM/yyyy")]
        public DateTime DataFim { get; set; }

        [CsvColumn(Name = "NM_EMPRESA", FieldIndex = 4)]
        public string NomeEmpresa { get; set; }

        [CsvColumn(Name = "NM_MESA", FieldIndex = 5)]
        public string NomeMesa { get; set; }

        [CsvColumn(Name = "NM_ESTRATEGIA", FieldIndex = 6)]
        public string NomeEstrategia { get; set; }

        [CsvColumn(Name = "NM_CENTRALIZADOR", FieldIndex = 7)]
        public string NomeCentralizador { get; set; }

        [CsvColumn(Name = "NM_GESTOR", FieldIndex = 8)]
        public string NomeGestor { get; set; }

        [CsvColumn(Name = "NM_SUBGESTOR", FieldIndex = 9)]
        public string NomeSubGestor { get; set; }

        [CsvColumn(Name = "NM_SUBPRODUTO", FieldIndex = 10)]
        public string NomeSubProduto { get; set; }

        [CsvColumn(Name = "NM_CARACTERISTICA", FieldIndex = 11)]
        public string NomeCaracteristica { get; set; }

        [CsvColumn(Name = "CD_ATIVO_OBJETO", FieldIndex = 12)]
        public string CodigoAtivoBoleto { get; set; }

        [CsvColumn(Name = "QUANTIDADE", FieldIndex = 13, CanBeNull = false)]
        public double Quantidade { get; set; }

        [CsvColumn(Name = "ID_PRECO", FieldIndex = 14, CanBeNull = false)]
        public long IdPreco { get; set; }

        public long NumeroPrazoDiasCorrigos
        {
            get
            {
                return (long)(DataFim - DataInicio).TotalDays;
            }

            private set { }
        }
    }
}
