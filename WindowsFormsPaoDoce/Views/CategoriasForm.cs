using MySql.Data.MySqlClient;
using System;
using System.Data;
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

        private void LimparCampos()
        {
           
            txtId.Clear();
            chkAtivo.Checked = false;
            txtNome.Clear();
        }
        private void CarregarCategorias()
        {
            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                try
                {
                    conn.Open();

                    string sql = "SELECT * FROM categorias";

                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvCategorias.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar: " + ex.Message);
                }
            }
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
            CarregarCategorias();
            txtId.Visible = false;
            TestarConexao();
        }

        private void btnSalvar_Click(object sender, System.EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                try
                {
                    conn.Open();

                    string sql = @"INSERT INTO categorias 
                           (nome, ativo, criado_em, atualizado_em) 
                           VALUES (@nome, @ativo, NOW() , NOW() )";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = txtNome.Text;
                        cmd.Parameters.Add("@ativo", MySqlDbType.Bit).Value = chkAtivo.Checked;
                       

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Salvo com sucesso!");
                    btnLimpar.Show();
                    CarregarCategorias();
                    LimparCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
            }
        }

   

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }

        private void gridDados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCategorias.Rows[e.RowIndex];

                txtId.Text = Convert.ToString(row.Cells["id"].Value);
                txtNome.Text = row.Cells["nome"].Value.ToString();
                chkAtivo.Checked = Convert.ToBoolean(row.Cells["ativo"].Value);
            }
        }

       

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void grpCadastro_Enter(object sender, EventArgs e)
        {

        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                conn.Open();

                string sql = @"UPDATE categorias 
SET nome = @nome, 
    atualizado_em = NOW(), 
    ativo = @ativo 
WHERE id = @id";

                try
                {


                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                        cmd.Parameters.AddWithValue("@ativo", chkAtivo.Checked);
                        cmd.Parameters.AddWithValue("@id", txtId.Text);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Atualizado!");
                    CarregarCategorias();
                    LimparCampos();


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao atualizar: " + ex.Message);
                }
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
                        cmd.Parameters.AddWithValue("@id", txtId.Text);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Excluído!");
                CarregarCategorias();
                LimparCampos();
            }
        }
    }
}
