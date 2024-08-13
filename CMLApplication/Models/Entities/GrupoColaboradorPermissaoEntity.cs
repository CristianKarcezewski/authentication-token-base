using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMLApplication.Models.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("grupos_colaboradores_permissoes")]
    public class GrupoColaboradorPermissaoEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("id_grupos_colaboradores")]
        public int IdGrupoColaborador { get; set; }

        [Column("id_permissoes")]
        public int IdPermissao { get; set; }

        [ForeignKey("IdPermissao")]
        public PermissaoEntity? Permissao { get; set; }

        [ForeignKey("IdGrupoColaborador")]
        public GrupoColaboradorEntity? GrupoColaborador { get; set; }
    }

}
