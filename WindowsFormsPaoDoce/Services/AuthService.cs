using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace WindowsFormsPaoDoce.Services
{
    public class AuthService
    {
        private class UsuarioFake
        {
            public long Id { get; set; }
            public string Nome { get; set; }
            public string Login { get; set; }
            public string SenhaHash { get; set; }
        }

        private readonly List<UsuarioFake> _usuarios = new List<UsuarioFake>
        {
            new UsuarioFake
            {
                Id = 1,
                Nome = "Administrador",
                Login = "admin",
                SenhaHash = GerarSha256("admin123")
            }
        };

        public bool TentarAutenticar(string login, string senha, out long usuarioId, out string nomeUsuario)
        {
            usuarioId = 0;
            nomeUsuario = string.Empty;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(senha))
            {
                return false;
            }

            string senhaHash = GerarSha256(senha.Trim());

            foreach (UsuarioFake usuario in _usuarios)
            {
                if (string.Equals(usuario.Login, login.Trim(), StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(usuario.SenhaHash, senhaHash, StringComparison.Ordinal))
                {
                    usuarioId = usuario.Id;
                    nomeUsuario = usuario.Nome;
                    return true;
                }
            }

            return false;
        }

        public static string GerarSha256(string texto)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(texto ?? string.Empty);
                byte[] hash = sha.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder(hash.Length * 2);
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
