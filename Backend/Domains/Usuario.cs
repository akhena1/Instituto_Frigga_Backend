using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domains
{
    public partial class Usuario
    {
        public Usuario()
        {
            Endereco = new HashSet<Endereco>();
            Oferta = new HashSet<Oferta>();
            Receita = new HashSet<Receita>();
        }

        [Key]
        [Column("Usuario_id")]
        public int UsuarioId { get; set; }
        [StringLength(255)]
        public string Nome { get; set; }
        [StringLength(255)]
        public string Telefone { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(255)]
        public string Biografia { get; set; }
        [StringLength(255)]
        public string Senha { get; set; }
        [Column("Cnpj_cpf")]
        [StringLength(14)]
        public string CnpjCpf { get; set; }
        [Column("Tipo_usuario_id")]
        public int? TipoUsuarioId { get; set; }

        [ForeignKey(nameof(TipoUsuarioId))]
        [InverseProperty("Usuario")]
        public virtual TipoUsuario TipoUsuario { get; set; }
        [InverseProperty("Usuario")]
        public virtual ICollection<Endereco> Endereco { get; set; }
        [InverseProperty("Usuario")]
        public virtual ICollection<Oferta> Oferta { get; set; }
        [InverseProperty("Usuario")]
        public virtual ICollection<Receita> Receita { get; set; }
    }
}
