using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMLApplication.Models.Entities
{
    [Table("permissoes")]
    public class PermissaoEntity
    {
        #region Propriedades

        [Key, Column("id")]
        public int Id { get; set; }

        [Required, Column("descricao")]
        public string? Descricao { get; set; }

        #endregion
    }
}
