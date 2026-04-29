using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace WindowsFormsPaoDoce.Views
{
    public partial class RelatorioEstoqueForm : Form
    {
        public RelatorioEstoqueForm()
        {
            InitializeComponent();
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

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable tabela = new DataTable();

                    adapter.Fill(tabela);

                    dataGridView1.DataSource = tabela;
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
                int atual = Convert.ToInt32(row.Cells["quantidade_atual"].Value);
                int minimo = Convert.ToInt32(row.Cells["estoque_minimo"].Value);

                if (atual <= minimo)
                {
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
                }
            }
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }
    }
    }

    

