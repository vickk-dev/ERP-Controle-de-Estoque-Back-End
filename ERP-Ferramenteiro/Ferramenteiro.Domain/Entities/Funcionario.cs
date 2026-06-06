using System;

namespace Ferramenteiro.Domain.Entities
{
    public class Funcionario
    {
        public Guid Id { get; private set; }
        public string Matricula { get; private set; }
        public string Nome { get; private set; }
        public bool Ativo { get; private set; }
        public string Email { get; private set; }
        public string SenhaHash { get; private set; }

        private Funcionario() { }

        public Funcionario(string matricula, string nome, string email, string senhaHash)
        {
            if (string.IsNullOrWhiteSpace(matricula)) throw new ArgumentException("Matrícula é obrigatória.");
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome é obrigatório.");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("E-mail é obrigatório.");
            if (string.IsNullOrWhiteSpace(senhaHash)) throw new ArgumentException("Hash da senha é obrigatório.");

            Id = Guid.NewGuid();
            Matricula = matricula;
            Nome = nome;
            Email = email.ToLowerInvariant();
            SenhaHash = senhaHash;
            Ativo = true;
        }

        public void Inativar() => Ativo = false;

        public void AtualizarSenhaHash(string novoHash)
        {
            if (string.IsNullOrWhiteSpace(novoHash)) throw new ArgumentException("Hash inválido.");
            SenhaHash = novoHash;
        }
    }
}