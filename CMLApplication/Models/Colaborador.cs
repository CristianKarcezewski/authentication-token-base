using CMLApplication.Models.Entities;

namespace CMLApplication.Models
{
    public class Colaborador
    {
        #region Propriedades

        public int Id { get; set; }
        public int IdGrupoColaborador { get; set; }
        public string? NomeCompleto { get; set; }
        public string? Login { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? Telefone { get; set; }
        public bool Active { get; set; }
        public DateTime? UltimaAtualizacao { get; set; }
        public DateTime? DTHR { get; set; }
        public List<Permissao>? Permissoes { get; set; }

        #endregion

        #region Construtores

        public Colaborador()
        {
            this.Permissoes = new List<Permissao>();
        }
        public Colaborador(ColaboradorEntity colaboradorEntity)
        {
            this.Id = colaboradorEntity.Id;
            this.IdGrupoColaborador = colaboradorEntity.IdGrupoColaborador;
            this.NomeCompleto = colaboradorEntity.NomeCompleto;
            this.Login = colaboradorEntity.Login;
            this.Email = colaboradorEntity.Email;
            this.Senha = colaboradorEntity.Chave.Senha;
            this.Telefone = colaboradorEntity.Telefone;
            this.Active = colaboradorEntity.Ativo;
            this.UltimaAtualizacao = colaboradorEntity.UltimaAtualizacao;
            this.DTHR = colaboradorEntity.DTHR;
            this.Permissoes = new List<Permissao>();
        }

        #endregion

        #region Propriedades Tratadas

        public string Nome
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(NomeCompleto))
                {
                    string[] split = NomeCompleto.Split(" ");
                    return split[0];
                }
                return string.Empty;
            }
        }

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
