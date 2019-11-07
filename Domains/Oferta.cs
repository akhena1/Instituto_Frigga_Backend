using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instituto_Frigga_Backend.Domains
{
    public partial class Oferta
    {
        [Key]
        [Column("Oferta_id")]
        public int OfertaId { get; set; }
        public double? Preco { get; set; }
        public double? Peso { get; set; }
        [Column("Imagem_produto")]
        [StringLength(255)]
        public string ImagemProduto { get; set; }
        public int? Quantidade { get; set; }
        [Column("Usuario_id")]
        public int? UsuarioId { get; set; }
        [Column("Produto_id")]
        public int? ProdutoId { get; set; }

        [ForeignKey(nameof(ProdutoId))]
        [InverseProperty("Oferta")]
        public virtual Produto Produto { get; set; }
        [ForeignKey(nameof(UsuarioId))]
        [InverseProperty("Oferta")]
        public virtual Usuario Usuario { get; set; }
    }
}
