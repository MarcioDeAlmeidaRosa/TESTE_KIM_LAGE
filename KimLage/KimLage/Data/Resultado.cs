using LINQtoCSV;


namespace KimLage.Data
{
    public class Resultado
    {
        [CsvColumn(Name = "NM_SUBPRODUTO", FieldIndex = 1)]
        public string NomeSubProduto { get; set; }

        [CsvColumn(Name = "RESULTADO", FieldIndex = 2, CanBeNull = false)]
        public double Total { get; set; }
    }
}
