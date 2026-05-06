using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsPaoDoce.Views
{
    public partial class CadastroForm : Form
    {
        string conexao = "Server=62.171.167.217;Port=33060;Database=db_padaria;Uid=root_padaria;Pwd=Pad2026SecPad;";
        public CadastroForm()
        {
            InitializeComponent();
        }

        private void CadastroForm_Load(object sender, EventArgs e)
        {
            txtSenha.UseSystemPasswordChar = true;
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text;
            string senha = txtSenha.Text;
            string senhaHash = HashService.GerarHash(senha);
            

            try
            { 
                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    conn.Open();

                    string sql = "INSERT INTO usuarios (nome,login, senha_hash) VALUES (@nome, @login, @senha)";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@nome", login);
                    cmd.Parameters.AddWithValue("@senha", senhaHash);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Usuario Cadastrado com sucesso!");

                    LoginForm tela = new LoginForm();
                    tela.Show();
                    this.Hide();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }

        }

        private void chkMostrarSenha_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMostrarSenha.Checked)
            {
                txtSenha.UseSystemPasswordChar = false;
            }
            else
            {
                txtSenha.UseSystemPasswordChar= true;
            }
        }
        public static class HashService
        {
            public static string GerarHash(string texto)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] bytes =
                        sha256.ComputeHash(Encoding.UTF8.GetBytes(texto));
                    StringBuilder builder = new StringBuilder();

                    foreach (byte b in bytes)
                        builder.Append(b.ToString("x2"));

                    return builder.ToString();

                }
            }
        }
    }
    
}
