using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMLApplication.Models.Entities
{
    [Table("colaboradores")]
    public class ColaboradorEntity
    {
        #region Propriedades

        [Key, Column("id")]
        public int Id { get; set; }

        [Column("id_chave")]
        public int IdChave { get;set; }

        [Required, Column("id_grupos_colaboradores")]
        public int IdGrupoColaborador { get; set; }

        [Required, Column("login")]
        public string? Login { get; set; }

        [Required, Column("nome_completo")]
        public string? NomeCompleto { get; set; }

        [Required, Column("email")]
        public string? Email { get; set; }

        [Column("telefone")]
        public string? Telefone { get; set; }

        [Column("ativo")]
        public bool Ativo {  get; set; }

        [Required, Column("ultima_atualizacao")]
        public DateTime? UltimaAtualizacao { get; set; }

        [Required, Column("dthr")]
        public DateTime? DTHR { get; set; }

        #endregion

        #region Chaves Estrangeiras

        [ForeignKey("Id")]
        public GrupoColaboradorEntity? GrupoColaborador { get; set; }

        [ForeignKey("Id")]
        public Chave? Chave { get; set; }

        #endregion

        public ColaboradorEntity() { }
        public ColaboradorEntity(Colaborador colaborador)
        {
            this.Id = colaborador.Id;
            this.Login = colaborador.Login;
            this.NomeCompleto = colaborador.NomeCompleto;
            this.Email = colaborador.Email;
            this.Telefone = colaborador.Telefone;
            this.Ativo = colaborador.Active;
            this.UltimaAtualizacao = colaborador.UltimaAtualizacao;
            this.DTHR = colaborador.DTHR;
        }

        #region Propriedades Tratadas

        [NotMapped]
        public string Nome
        {
            get {
                if (!string.IsNullOrWhiteSpace(NomeCompleto))
                {
                    string[] split = NomeCompleto.Split(" ");
                    return split[0];
                }
                return string.Empty;
            }
        }

        [NotMapped]
        public string Sobrenome
        {
            get
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

        #endregion
    }
}
