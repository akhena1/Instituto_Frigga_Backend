using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domains
{
    public partial class Endereco
    {
        [Key]
        [Column("Endereco_id")]
        public int EnderecoId { get; set; }
        [StringLength(255)]
        public string Nome { get; set; }
        [StringLength(10)]
        public string Numero { get; set; }
        [StringLength(100)]
        public string Bairro { get; set; }
        [StringLength(255)]
        public string Complemento { get; set; }
        [StringLength(8)]
        public string Cep { get; set; }
        [StringLength(255)]
        public string Cidade { get; set; }
        [Column("Usuario_id")]
        public int? UsuarioId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        [InverseProperty("Endereco")]
        public virtual Usuario Usuario { get; set; }
    }
}
