using System;

namespace WindowsFormsPaoDoce.Models
{
    public class Movimentacao
    {
        public long Id { get; set; }
        public long ProdutoId { get; set; }
        public TipoMovimentacao Tipo { get; set; }
        public int Quantidade { get; set; }
        public string Motivo { get; set; }
        public long? UsuarioId { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}
