using MongoDB.Driver;
using TechsysLog.Domain.Entities;
using TechsysLog.Domain.Interfaces;
using TechsysLog.Infra.Data.Context;

namespace TechsysLog.Infra.Data.Repositories
{
    /// <summary>
    /// Repositório de usuários utilizando MongoDB.
    /// Responsável por operações de CRUD e desativação lógica.
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IMongoCollection<Usuario> _usuarios;

        /// <summary>
        /// Construtor que inicializa a coleção de usuários.
        /// </summary>
        /// <param name="context">Contexto do MongoDB contendo a coleção de usuários.</param>
        public UsuarioRepository(MongoDbContext context)
        {
            _usuarios = context.Usuarios;
        }

        /// <summary>
        /// Adiciona um novo usuário.
        /// </summary>
        /// <param name="usuario">Objeto usuário a ser inserido.</param>
        /// <param name="ct">Token de cancelamento da operação.</param>
        public async Task AddAsync(Usuario usuario, CancellationToken ct)
            => await _usuarios.InsertOneAsync(usuario, cancellationToken: ct);

        /// <summary>
        /// Obtém um usuário pelo identificador único.
        /// </summary>
        /// <param name="id">Identificador do usuário.</param>
        /// <param name="ct">Token de cancelamento da operação.</param>
        /// <returns>Objeto usuário ou null se não encontrado.</returns>
        public async Task<Usuario?> ObterPorIdAsync(Guid id, CancellationToken ct)
            => await _usuarios.Find(u => u.Id == id).FirstOrDefaultAsync(ct);

        /// <summary>
        /// Obtém um usuário pelo e-mail.
        /// </summary>
        /// <param name="email">E-mail do usuário.</param>
        /// <param name="ct">Token de cancelamento da operação.</param>
        /// <returns>Objeto usuário ou null se não encontrado.</returns>
        public async Task<Usuario?> ObterPorEmailAsync(string email, CancellationToken ct)
            => await _usuarios.Find(u => u.Email == email).FirstOrDefaultAsync(ct);

        /// <summary>
        /// Lista todos os usuários cadastrados.
        /// </summary>
        /// <param name="ct">Token de cancelamento da operação.</param>
        /// <returns>Lista de usuários.</returns>
        public async Task<IEnumerable<Usuario>> ListarTodosAsync(CancellationToken ct)
            => await _usuarios.Find(Builders<Usuario>.Filter.Empty).ToListAsync(ct);

        /// <summary>
        /// Atualiza os dados de um usuário existente.
        /// </summary>
        /// <param name="usuario">Objeto usuário atualizado.</param>
        /// <param name="ct">Token de cancelamento da operação.</param>
        public async Task UpdateAsync(Usuario usuario, CancellationToken ct)
            => await _usuarios.ReplaceOneAsync(u => u.Id == usuario.Id, usuario, cancellationToken: ct);

        /// <summary>
        /// Desativa um usuário (remoção lógica).
        /// </summary>
        /// <param name="id">Identificador do usuário.</param>
        /// <param name="ct">Token de cancelamento da operação.</param>
        public async Task DesativarAsync(Guid id, CancellationToken ct)
        {
            var update = Builders<Usuario>.Update
                .Set(u => u.Ativo, false)
                .Set(u => u.DataAtualizacao, DateTime.UtcNow);

            await _usuarios.UpdateOneAsync(u => u.Id == id, update, cancellationToken: ct);
        }
    }
}
