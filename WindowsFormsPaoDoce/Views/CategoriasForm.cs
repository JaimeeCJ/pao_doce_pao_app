using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace WindowsFormsPaoDoce.Views
{
    public partial class CategoriasForm : Form
    {
        string conexao = "Server=62.171.167.217;Port=33060;Database=db_padaria;Uid=root_padaria;Pwd=Pad2026SecPad;";
        public CategoriasForm()
        {
            InitializeComponent();
        }

        private void TestarConexao()
        {


            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                try
                {
                    conn.Open();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
            }
        }
        private void CategoriasForm_Load(object sender, System.EventArgs e)
        {
            txtId.Visible = false;
            gridDados();
        }

        private void btnSalvar_Click(object sender, System.EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                try
                {
                    conn.Open();

                    string sql = @"INSERT INTO categorias 
                           (nome, descricao, ativo) 
                           VALUES (@nome, @descricao, @ativo)";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = txtNome.Text;
                        cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = txtDescricao.Text;
                        cmd.Parameters.Add("@ativo", MySqlDbType.Bit).Value = chkAtivo.Checked;

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Salvo com sucesso!");
                    btnLimpar.Show();
                    CarregarGrid(); // opcional
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                conn.Open();

                string sql = @"UPTADE categorias SET nome=@nome, descriçao=@descriçao, ativo=@ativo WHERE id=@id";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@descriçao", txtDescricao.Text);
                    cmd.Parameters.AddWithValue("@ativo", chkAtivo.Text);
                    cmd.Parameters.AddWithValue("@id", txtId);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Atualizado!");
                gridDados();
        }
    }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }

        private void gridDados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            using (MySqlConnection coon = new MySqlConnection(conexao))
            {
                coon.Open();

                string sql = "SELECT * FROM categorias";

                MySqlDataAdapter da = new MySqlDataAdapter(sql, coon);
                DataTable dt = new DataTable();
                da.Fill(dt);

                DataGridView.DataSource = dt;
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir?", "Confirmção", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (MySqlConnection coon = new MySqlConnection(conexao))
                {
                    coon.Open();

                    string sql = "DELETE FROM categorias WHERE id=@id";

                    using (MySqlCommand cmd = new MySqlCommand(sql, coon))
                    {
                        cmd.Parameters.AddWithValue("@id", txtId);
                        cmd.ExecuteNonQuery ();
                    }
                }

                MessageBox.Show("Excluído!");
                gridDados();
            }


        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNome.Clear();
            txtDescricao.Clear();
            chkAtivo.Checked = false;
        }

        private void grpCadastro_Enter(object sender, EventArgs e)
        {

        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {

        }
    }
}
