using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domains
{
    [Table("Categoria_produto")]
    public partial class CategoriaProduto
    {
        public CategoriaProduto()
        {
            Produto = new HashSet<Produto>();
        }

        [Key]
        [Column("Categoria_produto_id")]
        public int CategoriaProdutoId { get; set; }
        [Column("Tipo_produto")]
        [StringLength(255)]
        public string TipoProduto { get; set; }

        [InverseProperty("CategoriaProduto")]
        public virtual ICollection<Produto> Produto { get; set; }
    }
}
