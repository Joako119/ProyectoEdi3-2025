using GestionCompeticiones.Abstractions;

namespace GestionCompeticiones.Services.AuthServices
{
    internal class TokenParameters : ITokensParameters
    {
        public string Id { get; set; }
        public string PaswordHash { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
    }
}