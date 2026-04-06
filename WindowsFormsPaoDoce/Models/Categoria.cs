using System;

namespace WindowsFormsPaoDoce.Models
{
    public class Categoria
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AtualizadoEm { get; set; }
    }
}
