using TechsysLog.Domain.Interfaces;

namespace TechsysLog.Application.Commands.Usuarios;

/// <summary>
/// Comando para atualizar os dados de um usuário existente.
/// </summary>
public class AtualizarUsuarioCommand : ICommand
{
    /// <summary>
    /// Identificador único do usuário.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Nome atualizado do usuário.
    /// </summary>
    public string Nome { get; }

    /// <summary>
    /// Email atualizado do usuário.
    /// </summary>
    public string Email { get; }

    /// <summary>
    /// Nova senha do usuário (opcional, pode ser nula).
    /// </summary>
    public string? Senha { get; }

    /// <summary>
    /// Construtor do comando de atualização de usuário.
    /// </summary>
    /// <param name="id">Identificador único do usuário.</param>
    /// <param name="nome">Nome atualizado.</param>
    /// <param name="email">Email atualizado.</param>
    /// <param name="senha">Nova senha (opcional).</param>
    public AtualizarUsuarioCommand(Guid id, string nome, string email, string? senha = null)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Senha = senha;
    }
}
