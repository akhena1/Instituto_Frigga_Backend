using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domains
{
    [Table("Tipo_usuario")]
    public partial class TipoUsuario
    {
        public TipoUsuario()
        {
            Usuario = new HashSet<Usuario>();
        }

        [Key]
        [Column("Tipo_usuario_id")]
        public int TipoUsuarioId { get; set; }
        [StringLength(255)]
        public string Tipo { get; set; }

        [InverseProperty("TipoUsuario")]
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
