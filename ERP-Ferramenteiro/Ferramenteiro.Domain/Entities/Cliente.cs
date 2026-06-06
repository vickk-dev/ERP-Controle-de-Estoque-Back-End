using Ferramenteiro.Domain.Enums;

namespace Ferramenteiro.Domain.Entities
{

    public class Cliente
    {
        public Guid Id { get; private set; }
        public TipoDocumento TipoDocumento { get; private set; }
        public string Documento { get; private set; }
        public string NomeRazaoSocial { get; private set; }
        public string? Telefone { get; private set; }

        public string EnderecoCompleto { get; private set; }

        public DateTime CriadoEm { get; private set; }

        private Cliente() { }

        public Cliente(
            TipoDocumento tipoDocumento,
            string documento,
            string nomeRazaoSocial,
            string enderecoCompleto,
            string? telefone = null)
        {
            Id = Guid.NewGuid();
            TipoDocumento = tipoDocumento;
            Documento = documento;
            NomeRazaoSocial = nomeRazaoSocial;
            EnderecoCompleto = enderecoCompleto;
            Telefone = telefone;
            CriadoEm = DateTime.UtcNow;
        }
    }
}