namespace ERP_Ferramenteiro.Application.Interfaces
{
    /// <summary>
    /// Abstração para hashing e verificação de senhas.
    /// Implementação concreta usa BCrypt.
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>Gera o hash BCrypt da senha em texto puro.</summary>
        string Hash(string senhaPlana);

        /// <summary>Verifica se a senha em texto puro confere com o hash armazenado.</summary>
        bool Verify(string senhaPlana, string hash);
    }
}