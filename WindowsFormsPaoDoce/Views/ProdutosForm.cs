using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;


namespace WindowsFormsPaoDoce.Views
{
    public partial class ProdutosForm : Form
    {

       
        string conexao = "Server=62.171.167.217;Port=33060;Database=db_padaria;Uid=root_padaria;Pwd=Pad2026SecPad;";
        public ProdutosForm()
        {

            InitializeComponent();
        }

        private void LimparCampos()
        {
            txtId.Clear();
            txtNomeProduto.Clear();
            txtDescricao.Clear();
            txtPreco.Clear();
            txtQuantidade.Clear();
            txtEstoqueMinimo.Clear();
            txtBusca.Clear(); 

            chkAtivo.Checked = false;

            txtNomeProduto.Focus(); 
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

        private void ListarProdutos()
        {
            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                conn.Open();

                string sql = "SELECT * FROM produtos";

                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();

                da.Fill(dt);
                dgvProdutos.DataSource = dt;
            }
        }



        private void lblTitulo_Click(object sender, System.EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            txtDescricaoProduto.Clear();
            txtNomeProduto.Clear();
            txtPreco.Clear();
            txtEstoqueMinimo.Clear();
            txtQuantidade.Clear();
        }

        private void groupBox1_Enter(object sender, System.EventArgs e)
        {

        }

        private void txtId_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void ProdutosForm_Load(object sender, System.EventArgs e)
        {
           
            TestarConexao();
            ListarProdutos();

            txtId.Visible = false;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            string nome = txtNomeProduto.Text.Trim();
           
            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                try
                {
                    conn.Open();
                    string sql = @"INSERT INTO produtos
                 (nome, descricao, preco_unitario, quantidade_atual, estoque_minimo, ativo, categoria_id, criado_em, atualizado_em)
                 VALUES
                  (@nome, @descricao, @preco, @quantidade, @estoqueMin, @ativo, @categoria, NOW(), NOW())";


                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@descricao", txtDescricao.Text);
                    cmd.Parameters.AddWithValue("@preco", decimal.Parse(txtPreco.Text));
                    cmd.Parameters.AddWithValue("@quantidade", int.Parse(txtQuantidade.Text));
                    cmd.Parameters.AddWithValue("@estoqueMin", int.Parse(txtEstoqueMinimo.Text));
                    cmd.Parameters.AddWithValue("@ativo", chkAtivo.Checked);
                    cmd.Parameters.AddWithValue("@categoria", 1);


                    if (string.IsNullOrWhiteSpace(txtNomeProduto.Text))
                    {
                        MessageBox.Show("Digite o nome do produto!");
                        return;
                    }

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Produto Cadastrado com sucesso!");

                    ListarProdutos();
                    LimparCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(" Erro! " + ex.Message);
                }
            }
        }
        
        private void label1_Click(object sender, EventArgs e)
        {

        }
   
        private void dgvProdutos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
       
            if (e.RowIndex >= 0)
            {
                txtId.Text = dgvProdutos.Rows[e.RowIndex].Cells["id"].Value.ToString();
                txtNomeProduto.Text = dgvProdutos.Rows[e.RowIndex].Cells["nome"].Value.ToString();
                txtDescricao.Text = dgvProdutos.Rows[e.RowIndex].Cells["descricao"].Value.ToString();
                txtPreco.Text = dgvProdutos.Rows[e.RowIndex].Cells["preco_unitario"].Value.ToString();
                txtQuantidade.Text = dgvProdutos.Rows[e.RowIndex].Cells["quantidade_atual"].Value.ToString();
                txtEstoqueMinimo.Text = dgvProdutos.Rows[e.RowIndex].Cells["estoque_minimo"].Value.ToString();
                chkAtivo.Checked = Convert.ToBoolean(dgvProdutos.Rows[e.RowIndex].Cells["ativo"].Value);
            }
        
    }

        private void btnAtualizar_Click_1(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                try
                {
                    conn.Open();

                    string sql = @"UPDATE produtos SET 
                           nome = @nome,
                           descricao = @descricao,
                           preco_unitario = @preco,
                           quantidade_atual = @quantidade,
                           estoque_minimo = @estoqueMin,
                           ativo = @ativo,
                           atualizado_em = NOW()
                           WHERE id = @id";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@id", int.Parse(txtId.Text));
                    cmd.Parameters.AddWithValue("@nome", txtNomeProduto.Text);
                    cmd.Parameters.AddWithValue("@descricao", txtDescricao.Text);
                    cmd.Parameters.AddWithValue("@preco", decimal.Parse(txtPreco.Text));
                    cmd.Parameters.AddWithValue("@quantidade", int.Parse(txtQuantidade.Text));
                    cmd.Parameters.AddWithValue("@estoqueMin", int.Parse(txtEstoqueMinimo.Text));
                    cmd.Parameters.AddWithValue("@ativo", chkAtivo.Checked);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Produto atualizado com sucesso!");
                    ListarProdutos();
                    LimparCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
            }
        }

        private void btnExclui_Click_1(object sender, EventArgs e)
        {
       
            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                try
                {
                    conn.Open();

                    string sql = "DELETE FROM produtos WHERE id = @id";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", int.Parse(txtId.Text));

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Produto excluído com sucesso!");
                    ListarProdutos();
                    LimparCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
            }
    }

        private void btnBusca_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                try
                {
                    conn.Open();

                    string sql = @"SELECT * FROM produtos 
                           WHERE nome LIKE @busca";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@busca", "%" + txtBusca.Text + "%");

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvProdutos.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
            }
        }

        private void txtBusca_TextChanged(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                try
                {
                    conn.Open();

                    string sql = @"SELECT * FROM produtos 
                           WHERE nome LIKE @busca";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@busca", "%" + txtBusca.Text + "%");

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvProdutos.DataSource = dt;
                }
                catch
                {
                    
                }
            }
        }
    }
}
