using LINQtoCSV;

namespace KimLage.Data
{
    public class Mercado
    {
        [CsvColumn(Name = "ID_PRECO", FieldIndex = 1, CanBeNull = false)]
        public long IdPreco { get; set; }

        [CsvColumn(Name = "NU_PRAZO_DIAS_CORRIDOS", FieldIndex = 2)]
        public long NumeroPrazoDiasCorrigos { get; set; }

        [CsvColumn(Name = "VL_PRECO", FieldIndex = 3, CanBeNull = false)]
        public double ValorPreco { get; set; }
    }
}
