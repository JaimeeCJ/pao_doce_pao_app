using MySql.Data.MySqlClient;
using System;
using System.CodeDom.Compiler;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using Mysqlx.Connection;
using System.Collections.Specialized;

namespace WindowsFormsPaoDoce.Views
{
    public partial class ProdutosForm : Form
    {

       
        string conexao = "Server=62.171.167.217;Port=33060;Database=db_padaria;Uid=root_padaria;Pwd=Pad2026SecPad;";
        public ProdutosForm()
        {

            InitializeComponent();
        }

        private void CarregarCategorias()
        {
            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                conn.Open();

                string sql = "Select id, nome FROM categorias  WHERE ativo = 1";

                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);


                cmbCategoria.DataSource = dt;
                cmbCategoria.DisplayMember = "nome"; 
                cmbCategoria.ValueMember = "id";

            }
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

                dgvProdutos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgvProdutos.Columns["id"].HeaderText = "Código";
                dgvProdutos.Columns["nome"].HeaderText = "Nome";
                dgvProdutos.Columns["ativo"].HeaderText = "Ativo";
                dgvProdutos.Columns["estoque_minimo"].HeaderText = "Estoque Mínimo";
                dgvProdutos.Columns["descricao"].HeaderText = "Descrição";
                dgvProdutos.Columns["preco_unitario"].HeaderText = "Preço";
                dgvProdutos.Columns["preco_unitario"].DefaultCellStyle.Format = "C2";
                dgvProdutos.Columns["categoria_id"].HeaderText = "Código Categoria";
                dgvProdutos.Columns["criado_em"].HeaderText = "Criado em";
                dgvProdutos.Columns["atualizado_em"].HeaderText = "Atualizado em";

                dgvProdutos.Columns["quantidade_atual"].Visible = false;
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
            
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            TestarConexao();
            ListarProdutos();
            CarregarCategorias();
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
                    cmd.Parameters.AddWithValue("@descricao", txtDescricaoProduto.Text);
                    cmd.Parameters.AddWithValue("@preco", decimal.Parse(txtPreco.Text, NumberStyles.Currency));
                    cmd.Parameters.AddWithValue("@quantidade", int.Parse(txtQuantidade.Text));
                    cmd.Parameters.AddWithValue("@estoqueMin", int.Parse(txtEstoqueMinimo.Text));
                    cmd.Parameters.AddWithValue("@ativo", chkAtivo.Checked);
                    cmd.Parameters.AddWithValue("@categoria", cmbCategoria.SelectedValue);


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
                txtDescricaoProduto.Text = dgvProdutos.Rows[e.RowIndex].Cells["descricao"].Value.ToString();
                txtPreco.Text = dgvProdutos.Rows[e.RowIndex].Cells["preco_unitario"].Value.ToString();
                txtQuantidade.Text = dgvProdutos.Rows[e.RowIndex].Cells["quantidade_atual"].Value.ToString();
                txtEstoqueMinimo.Text = dgvProdutos.Rows[e.RowIndex].Cells["estoque_minimo"].Value.ToString();
                chkAtivo.Checked = Convert.ToBoolean(dgvProdutos.Rows[e.RowIndex].Cells["ativo"].Value);
            }
        
    }

        private void btnAtualizar_Click_1(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Selecione um produto válido.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNomeProduto.Text))
            {
                MessageBox.Show("O nome do produto é obrigatório.");
                return;
            }
            if (!decimal.TryParse(txtPreco.Text, NumberStyles.Currency, new CultureInfo("pt-BR"), out decimal preco))
            {
                MessageBox.Show("Preço inválido.");
                return;
            }

            if (!int.TryParse(txtQuantidade.Text, out int quantidade))
            {
                MessageBox.Show("Quantidade inválida.");
                return;
            }

            if (!int.TryParse(txtEstoqueMinimo.Text, out int estoqueMin))
            {
                MessageBox.Show("Estoque mínimo inválido.");
                return;
            }

            
            DialogResult resultado = MessageBox.Show(
                $"Deseja atualizar o produto:\n\nID: {id}\nNome: {txtNomeProduto.Text}?",
                "Confirmar atualização",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado != DialogResult.Yes)
                return;

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

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@nome", txtNomeProduto.Text);
                    cmd.Parameters.AddWithValue("@descricao", txtDescricao.Text);
                    cmd.Parameters.AddWithValue("@preco", preco);
                    cmd.Parameters.AddWithValue("@quantidade", quantidade);
                    cmd.Parameters.AddWithValue("@estoqueMin", estoqueMin);
                    cmd.Parameters.AddWithValue("@ativo", chkAtivo.Checked);
                   
                    int linhasAfetadas = cmd.ExecuteNonQuery();

                    if (linhasAfetadas > 0)
                    {
                        MessageBox.Show("Produto atualizado com sucesso!");
                        ListarProdutos();
                        LimparCampos();
                    }
                    else
                    {
                        MessageBox.Show("Nenhum produto foi atualizado. Verifique o ID.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro real: " + ex.Message);
                }
            }
        
    }
        

        private void btnExclui_Click_1(object sender, EventArgs e)
        {

            DialogResult resultado = MessageBox.Show(
               " Tem certeza que deseja excluir?"," Confirmar exclusão",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Warning
               );

            if (resultado != DialogResult.Yes)
                return;
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
                    MessageBox.Show("Erro real: " + ex.Message);
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

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Close();
            
        }

        private void txtNomeProduto_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPreco_Leave(object sender, EventArgs e)
        {
            
        }

        private void txtPreco_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtPreco_TextChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtPreco.Text))
                return;

            txtPreco.TextChanged -= txtPreco_TextChanged;

            string numeros = new string(txtPreco.Text.Where(char.IsDigit).ToArray());

            if (string.IsNullOrEmpty(numeros))
            {
                txtPreco.Text = "";
            }
            else
            {
                decimal valor = decimal.Parse(numeros) / 100;

                txtPreco.Text = valor.ToString("C2", new CultureInfo("pt-BR"));
                txtPreco.SelectionStart = txtPreco.Text.Length;
            }

            txtPreco.TextChanged += txtPreco_TextChanged;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
