using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMLApplication.Models.Entities
{
    [Table("chaves_encriptografadas")]
    public class Chave
    {
        #region Propriedades

        [Key, Column("id")]
        public int Id { get; set; }

        [Column("senha")]
        public string? Senha { get; set; }

        [Column("ativo")]
        public bool Ativo { get; set; }

        [Column("dthr")]
        public DateTime? DTHR { get; set; }

        #endregion

        #region Chaves Estrangeiras

        [ForeignKey("IdColaborador")]
        public PermissaoEntity? Permissao { get; set; }

        #endregion
    }
}
