using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instituto_Frigga_Backend.Domains
{
    public partial class Receita
    {
        [Key]
        [Column("Receita_id")]
        public int ReceitaId { get; set; }
        [StringLength(255)]
        public string Nome { get; set; }
        [Column(TypeName = "text")]
        public string Ingredientes { get; set; }
        [Column("Modo_de_preparo", TypeName = "text")]
        public string ModoDePreparo { get; set; }
        [Column("Imagem_receita")]
        [StringLength(255)]
        public string ImagemReceita { get; set; }
        [Column("Categoria_receita_id")]
        public int? CategoriaReceitaId { get; set; }
        [Column("Usuario_id")]
        public int? UsuarioId { get; set; }

        [ForeignKey(nameof(CategoriaReceitaId))]
        [InverseProperty("Receita")]
        public virtual CategoriaReceita CategoriaReceita { get; set; }
        [ForeignKey(nameof(UsuarioId))]
        [InverseProperty("Receita")]
        public virtual Usuario Usuario { get; set; }
    }
}
