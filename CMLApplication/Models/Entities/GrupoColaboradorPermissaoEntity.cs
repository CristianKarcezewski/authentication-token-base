using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMLApplication.Models.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("grupos_colaboradores_permissoes")]
    public class GrupoColaboradorPermissaoEntity
    {
        #region Propriedades
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required, Column("id_grupos_colaboradores")]
        public int IdGrupoColaborador { get; set; }

        [Required, Column("id_permissoes")]
        public int IdPermissao { get; set; }

        #endregion

        #region Chaves Estrangeiras

        [ForeignKey("IdPermissao")]
        public PermissaoEntity? Permissao { get; set; }

        [ForeignKey("IdGrupoColaborador")]
        public GrupoColaboradorEntity? GrupoColaborador { get; set; }

        #endregion
    }

}
