using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsPaoDoce.Core
{
    public static class AppTheme
    {
        public static readonly Color CorPrimaria = Color.FromArgb(219, 181, 95);
        public static readonly Color CorPrimariaEscura = Color.FromArgb(170, 134, 57);
        public static readonly Color CorFundo = Color.White;
        public static readonly Color CorTextoPrincipal = Color.FromArgb(33, 33, 33);
        public static readonly Color CorTextoSecundario = Color.FromArgb(90, 90, 90);
        public static readonly Color CorPerigo = Color.FromArgb(200, 50, 50);
        public static readonly Color CorSucesso = Color.FromArgb(50, 160, 70);

        public static void EstilizarFormularioPadrao(Form form)
        {
            form.BackColor = CorFundo;
            form.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            form.ForeColor = CorTextoPrincipal;
            form.StartPosition = FormStartPosition.CenterScreen;
        }

        public static Panel CriarCabecalho(string titulo, string subtitulo)
        {
            Panel cabecalho = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = CorPrimaria
            };

            Label lblTitulo = new Label
            {
                Text = titulo,
                AutoSize = true,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.Black,
                Location = new Point(20, 12)
            };

            Label lblSub = new Label
            {
                Text = subtitulo,
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(60, 60, 60),
                Location = new Point(24, 48)
            };

            cabecalho.Controls.Add(lblTitulo);
            cabecalho.Controls.Add(lblSub);
            return cabecalho;
        }

        public static Button CriarBotao(string texto, int largura, Color corFundo)
        {
            return new Button
            {
                Text = texto,
                Size = new Size(largura, 36),
                BackColor = corFundo,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = CorTextoPrincipal,
                Cursor = Cursors.Hand
            };
        }

        public static void EstilizarGrid(DataGridView grid)
        {
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.RowHeadersVisible = false;
            grid.BorderStyle = BorderStyle.FixedSingle;
            grid.BackgroundColor = Color.White;
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            grid.DefaultCellStyle.SelectionBackColor = CorPrimaria;
            grid.DefaultCellStyle.SelectionForeColor = Color.Black;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersHeight = 36;
            grid.RowTemplate.Height = 30;
        }
    }
}
