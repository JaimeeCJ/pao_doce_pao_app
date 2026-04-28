using MySql.Data.MySqlClient;
using System;
using System.Data;
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

                    string query = "SELECT * FROM vw_estoque_atual";

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
    }
}
