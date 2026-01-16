using MongoDB.Bson.Serialization.Attributes;

namespace TechsysLog.Domain.Entities
{
    public class Usuario
    {
        [BsonId]
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string SenhaHash { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataAtualizacao { get; private set; }

        public Usuario(Guid id, string nome, string email, string senhaHash)
        {
            Id = id;
            Nome = nome;
            Email = email;
            SenhaHash = senhaHash;
            Ativo = true;
            DataCriacao = DateTime.UtcNow;
        }

        public void AlterarNome(string nome) { Nome = nome; DataAtualizacao = DateTime.UtcNow; }
        public void AlterarEmail(string email) { Email = email; DataAtualizacao = DateTime.UtcNow; }
        public void AlterarSenha(string senhaHash) { SenhaHash = senhaHash; DataAtualizacao = DateTime.UtcNow; }
        public void Desativar() { Ativo = false; DataAtualizacao = DateTime.UtcNow; }
    }
}
