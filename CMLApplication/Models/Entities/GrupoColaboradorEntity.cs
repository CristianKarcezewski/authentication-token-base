using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMLApplication.Models.Entities
{
    [Table("grupos_colaboradores")]
    public class GrupoColaboradorEntity
    {
        #region Propriedades
        [Key, Column("id")]
        public int Id { get; set; }

        [Required, Column("nome")]
        public string? Nome { get; set; }

        #endregion
    }
}
