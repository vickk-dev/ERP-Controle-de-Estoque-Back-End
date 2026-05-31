using BCrypt.Net;
using ERP_Ferramenteiro.Application.Interfaces;

namespace ERP_Ferramenteiro.Infra.Security
{

    public sealed class BcryptPasswordHasher : IPasswordHasher
    {
        private const int WorkFactor = 12;

        public string Hash(string senhaPlana)
            => BCrypt.Net.BCrypt.HashPassword(senhaPlana, WorkFactor);

        public bool Verify(string senhaPlana, string hash)
            => BCrypt.Net.BCrypt.Verify(senhaPlana, hash);
    }
}