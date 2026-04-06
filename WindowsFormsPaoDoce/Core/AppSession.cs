namespace WindowsFormsPaoDoce.Core
{
    public static class AppSession
    {
        public static long? UsuarioId { get; private set; }
        public static string NomeUsuario { get; private set; }
        public static string LoginUsuario { get; private set; }

        public static bool IsAutenticado
        {
            get { return UsuarioId.HasValue; }
        }

        public static void IniciarSessao(long usuarioId, string nome, string login)
        {
            UsuarioId = usuarioId;
            NomeUsuario = nome;
            LoginUsuario = login;
        }

        public static void EncerrarSessao()
        {
            UsuarioId = null;
            NomeUsuario = null;
            LoginUsuario = null;
        }
    }
}
