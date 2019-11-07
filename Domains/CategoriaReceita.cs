using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instituto_Frigga_Backend.Domains
{
    [Table("Categoria_receita")]
    public partial class CategoriaReceita
    {
        public CategoriaReceita()
        {
            Receita = new HashSet<Receita>();
        }

        [Key]
        [Column("Categoria_receita_id")]
        public int CategoriaReceitaId { get; set; }
        [Column("Tipo_receita")]
        [StringLength(255)]
        public string TipoReceita { get; set; }

        [InverseProperty("CategoriaReceita")]
        public virtual ICollection<Receita> Receita { get; set; }
    }
}
