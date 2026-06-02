namespace ERP_Ferramenteiro.Domain.Entities
{
    public class Cliente
    {
        public Guid Id { get; private set; }
        public TipoCliente Tipo { get; private set; }
        public string Documento { get; private set; }
        public string NomeRazaoSocial { get; private set; }
        public string? NomeFantasia { get; private set; }
        public string Telefone { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string Cep { get; private set; }
        public DateTime DataCadastro { get; private set; }

        private Cliente() { }

        public Cliente(TipoCliente tipo, string documento, string nomeRazaoSocial, string telefone,
                       string logradouro, string numero, string bairro, string cidade, string estado, string cep, string? nomeFantasia = null)
        {
            if (string.IsNullOrWhiteSpace(documento)) throw new ArgumentException("Documento é obrigatório.");
            if (string.IsNullOrWhiteSpace(nomeRazaoSocial)) throw new ArgumentException("Nome/Razão Social é obrigatório.");

            if (string.IsNullOrWhiteSpace(cep) || string.IsNullOrWhiteSpace(logradouro))
                throw new ArgumentException("Endereço completo é obrigatório para logística.");

            Id = Guid.NewGuid();
            Tipo = tipo;
            Documento = documento;
            NomeRazaoSocial = nomeRazaoSocial;
            NomeFantasia = nomeFantasia;
            Telefone = telefone;
            Logradouro = logradouro;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Cep = cep;
            DataCadastro = DateTime.UtcNow;
        }

        public Cliente(string tipo, string documento, string nomeRazaoSocial, object nomeFantasia, string telefone, object logradouro, object numero, object bairro, object cidade, object estado, object cep)
        {
            Documento = documento;
            NomeRazaoSocial = nomeRazaoSocial;
            Telefone = telefone;
        }
    }
}