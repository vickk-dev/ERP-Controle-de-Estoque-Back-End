using System;

namespace ERP_Ferramenteiro.Domain.Entities
{
    public class Funcionario
    {
        public Guid Id { get; private set; }
        public string Matricula { get; private set; }
        public string Nome { get; private set; }
        public bool Ativo { get; private set; }

        private Funcionario() { }

        public Funcionario(string matricula, string nome)
        {
            if (string.IsNullOrWhiteSpace(matricula)) throw new ArgumentException("Matrícula é obrigatória.");
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome é obrigatório.");

            Id = Guid.NewGuid();
            Matricula = matricula;
            Nome = nome;
            Ativo = true; 
        }

        public void Inativar() => Ativo = false;
    }
}