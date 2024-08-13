using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMLApplication.Models.Entities
{
    [Table("permissoes")]
    public class PermissaoEntity
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("descricao")]
        public string? Descricao { get; set; }
    }
}
