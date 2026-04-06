using System;

namespace WindowsFormsPaoDoce.Models
{
    public class Produto
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public long CategoriaId { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int QuantidadeAtual { get; set; }
        public int EstoqueMinimo { get; set; }
        public bool Ativo { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AtualizadoEm { get; set; }
    }
}
