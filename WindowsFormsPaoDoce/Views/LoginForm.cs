using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WindowsFormsPaoDoce.Core;
using WindowsFormsPaoDoce.Services;

namespace WindowsFormsPaoDoce.Views
{
    public partial class LoginForm : Form
    {
        private readonly AuthService _authService = new AuthService();

        public LoginForm()
        {
            InitializeComponent();
            CarregarLogo();
        }

        private void CarregarLogo()
        {
            string caminho = BuscarArquivoLogo();
            if (!string.IsNullOrWhiteSpace(caminho))
            {
                pictureLogo.Image = Image.FromFile(caminho);
            }
        }

        private static string BuscarArquivoLogo()
        {
            string[] nomes = { "logo.jpg", "logo-circular.jpg" };
            DirectoryInfo atual = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            for (int nivel = 0; nivel < 5 && atual != null; nivel++)
            {
                string assetsDir = Path.Combine(atual.FullName, "assets");
                if (Directory.Exists(assetsDir))
                {
                    for (int i = 0; i < nomes.Length; i++)
                    {
                        string caminho = Path.Combine(assetsDir, nomes[i]);
                        if (File.Exists(caminho))
                            return caminho;
                    }
                }

                atual = atual.Parent;
            }

            return null;
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            long usuarioId;
            string nomeUsuario;
            bool autenticado = _authService.TentarAutenticar(txtLogin.Text, txtSenha.Text, out usuarioId, out nomeUsuario);
            if (!autenticado)
            {
                MessageBox.Show("Usuario ou senha invalidos.", "Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AppSession.IniciarSessao(usuarioId, nomeUsuario, txtLogin.Text.Trim());

            Hide();
            using (MainMenuForm menu = new MainMenuForm())
            {
                menu.ShowDialog();
            }

            Close();
        }
    }
}
