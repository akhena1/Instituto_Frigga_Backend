using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instituto_Frigga_Backend.Domains
{
    public partial class Produto
    {
        public Produto()
        {
            Oferta = new HashSet<Oferta>();
        }

        [Key]
        [Column("Produto_id")]
        public int ProdutoId { get; set; }
        [StringLength(255)]
        public string Tipo { get; set; }
        [Column("Categoria_produto_id")]
        public int? CategoriaProdutoId { get; set; }

        [ForeignKey(nameof(CategoriaProdutoId))]
        [InverseProperty("Produto")]
        public virtual CategoriaProduto CategoriaProduto { get; set; }
        [InverseProperty("Produto")]
        public virtual ICollection<Oferta> Oferta { get; set; }
    }
}
