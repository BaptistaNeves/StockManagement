namespace StockManagement.Core.Entities.Usuario
{
    public class Usuario : Entidade
    {
        public string NomeDeUtilizador { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; private set; }
        public string Senha { get; private set; }
        public string Endereco { get; private set; }
        public string Perfil { get; private set; }

        public Usuario(string nomeDeUtilizador, string email, string telefone, 
                       string senha, string endereco, string perfil = null)
        {
            NomeDeUtilizador = nomeDeUtilizador;
            Email = email;
            Telefone = telefone;
            Senha = senha;
            Endereco = endereco;
            Perfil = perfil;
            DefinirPerfilPadrao();
        }

        private void DefinirPerfilPadrao()
        {
            if(string.IsNullOrWhiteSpace(Perfil))
            {
                Perfil = "Moderador";
            }
        }
    }
}
