using MySql.Data.MySqlClient;
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
        string conexao = "Server=62.171.167.217;Port=33060;Database=db_padaria;Uid=root_padaria;Pwd=Pad2026SecPad;";
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
            string login = txtLogin.Text;
            string senha = txtSenha.Text;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(conexao)) 
                {
                    conn.Open();

                    string sql = "SELECT * FROM usuarios WHERE login = @login AND senha_hash = @senha";

                    MySqlCommand cmd = new MySqlCommand(sql, conn); 
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                 
                        MainMenuForm tela = new MainMenuForm();
                        tela.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Usuário ou senha inválidos!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtSenha.UseSystemPasswordChar = true;
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            CadastroForm form  = new CadastroForm();
            form.Show();
            this.Hide();
        }

        private void chkMostrarSenha_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMostrarSenha.Checked)
            {
                txtSenha.UseSystemPasswordChar = false;
            }
            else
            {
                txtSenha.UseSystemPasswordChar = true;
            }
        }
    }
}
