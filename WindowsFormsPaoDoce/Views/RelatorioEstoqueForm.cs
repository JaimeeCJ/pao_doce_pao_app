using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace WindowsFormsPaoDoce.Views
{
    public partial class RelatorioEstoqueForm : Form
    {

        // Tabela global
        private DataTable tabelaEstoque = new DataTable();

        public RelatorioEstoqueForm()
        {
            InitializeComponent();

            cmbFiltro.Items.Add("Produto");
            cmbFiltro.Items.Add("Categoria");
            cmbFiltro.Items.Add("Quantidade Atual");
            cmbFiltro.Items.Add("Estoque Mínimo");
            cmbFiltro.Items.Add("Preço Unitário");
            cmbFiltro.Items.Add("Valor Total");
        }
        private void CarregarEstoque()
        {
            string conexao = "Server=62.171.167.217;Port=33060;Database=db_padaria;Uid=root_padaria;Pwd=Pad2026SecPad;";

            using (MySqlConnection conn = new MySqlConnection(conexao))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT * FROM vw_quantidade_atual";

                    MySqlDataAdapter adapter =
                        new MySqlDataAdapter(query, conn);

                    tabelaEstoque.Clear();

                    adapter.Fill(tabelaEstoque);

                    dataGridView1.DataSource = tabelaEstoque;

                    dataGridView1.Columns["id"].Visible = false;

                    dataGridView1.Columns["produto"].HeaderText = "Produto";

                    dataGridView1.Columns["quantidade_atual"].HeaderText =
                        "Quantidade Atual";

                    dataGridView1.Columns["estoque_minimo"].HeaderText =
                        "Estoque Mínimo";

                    dataGridView1.Columns["preco_unitario"].HeaderText =
                        "Preço Unitário";

                    dataGridView1.Columns["valor_total"].HeaderText =
                        "Valor Total";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }


            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void RelatorioEstoqueForm_Load(object sender, EventArgs e)
        {
            CarregarEstoque();
        }

        

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var row = dataGridView1.Rows[e.RowIndex];

            if (!row.IsNewRow &&
                row.Cells["quantidade_atual"].Value != DBNull.Value &&
                row.Cells["estoque_minimo"].Value != DBNull.Value)
            {
                int atual =
                    Convert.ToInt32(row.Cells["quantidade_atual"].Value);

                int minimo =
                    Convert.ToInt32(row.Cells["estoque_minimo"].Value);

                if (atual <= minimo)
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                string texto = txtPesquisar.Text.Trim();
                string filtro = cmbFiltro.SelectedItem?.ToString() ?? "Produto";

                DataView dv = tabelaEstoque.DefaultView;


                dataGridView1.DataSource = dv;

                switch (filtro)
                {
                    case "Produto":
                        dv.RowFilter = $"produto LIKE '%{texto}%'";
                        break;
                    case "Categoria":
                        dv.RowFilter = $"categoria LIKE '%{texto}%'";
                        break;
                    case "Quantidade Atual":
                        dv.RowFilter = $"quantidade_atual = {texto}";
                        break;
                    case "Estoque Mínimo":
                        dv.RowFilter = $"estoque_minimo = {texto}";
                        break;
                    case "Preço Unitário":
                        dv.RowFilter = $"preco_unitario = {texto}";
                        break;
                    case "Valor Total":
                        dv.RowFilter = $"valor_total = {texto}";
                        break;
                    default:
                        dv.RowFilter = $"produto LIKE '%{texto}%'";
                        break;

                      
                }
                dataGridView1.DataSource = dv;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na pesquisa: " + ex.Message);
            }

           
           
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtPesquisar.Clear();

            tabelaEstoque.DefaultView.RowFilter = "";

            dataGridView1.DataSource = tabelaEstoque;
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            MainMenuForm menu = new MainMenuForm();

            menu.Show();

            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
    }

    

