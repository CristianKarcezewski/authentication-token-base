using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMLApplication.Models.Entities
{
    [Table("colaboradores")]
    public class ColaboradorEntity
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("id_grupos_colaboradores")]
        public int IdGrupoColaborador { get; set; }

        [Column("login")]
        public string? Login { get; set; }

        [Column("nome_completo")]
        public string? NomeCompleto { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("telefone")]
        public string? Telefone { get; set; }

        [Column("ativo")]
        public bool Ativo {  get; set; }

        [Column("ultima_atualizacao")]
        public DateTime? UltimaAtualizacao { get; set; }

        [Column("dthr")]
        public DateTime? DTHR { get; set; }

        [ForeignKey("Id")]
        public GrupoColaboradorEntity? GrupoColaborador { get; set; }

        public string Nome()
        {
            if (!string.IsNullOrWhiteSpace(NomeCompleto))
            {
                string[] split = NomeCompleto.Split(" ");
                return split[0];
            }
            return string.Empty;
        }

        public string Sobrenome()
        {
            if (!string.IsNullOrWhiteSpace(NomeCompleto))
            {
                string[] split = NomeCompleto.Split(" ");
                if (split.Length > 1)
                {
                    return split[(split.Length - 1)];
                }
            }
            return string.Empty;
        }
    }
}
