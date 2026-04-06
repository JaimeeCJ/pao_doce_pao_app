using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WindowsFormsPaoDoce.Core;

namespace WindowsFormsPaoDoce.Views
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
            CarregarLogo();
            lblUsuario.Text = "Controle de Estoque — " + (AppSession.NomeUsuario ?? "N/A");
            MontarBotoesModulo();
        }

        private void MontarBotoesModulo()
        {
            int coluna1 = 0;
            int coluna2 = 380;
            int linha1 = 0;
            int linha2 = 130;
            int linha3 = 260;
            Size tamBotao = new Size(350, 110);

            painelBotoes.Controls.Add(CriarCardModulo(
                "Cadastro de Categorias", "Artur",
                tamBotao, new Point(coluna1, linha1), () => new CategoriasForm().ShowDialog()));

            painelBotoes.Controls.Add(CriarCardModulo(
                "Cadastro de Produtos", "Diogo",
                tamBotao, new Point(coluna2, linha1), () => new ProdutosForm().ShowDialog()));

            painelBotoes.Controls.Add(CriarCardModulo(
                "Movimentacao de Estoque", "Jaime",
                tamBotao, new Point(coluna1, linha2), () => new MovimentacaoEstoqueForm().ShowDialog()));

            painelBotoes.Controls.Add(CriarCardModulo(
                "Relatorio de Estoque", "Allan",
                tamBotao, new Point(coluna2, linha2), () => new RelatorioEstoqueForm().ShowDialog()));

            painelBotoes.Controls.Add(CriarCardModulo(
                "Exemplo de CRUD", "Referencia — pode deletar depois",
                tamBotao, new Point(coluna1, linha3), () => new FormExemplo().ShowDialog()));
        }

        private static Control CriarCardModulo(string titulo, string descricao, Size tamanho, Point posicao, Action abrirTela)
        {
            Panel card = new Panel
            {
                Size = tamanho,
                Location = posicao,
                BackColor = Color.FromArgb(250, 250, 250),
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand
            };

            Panel barraCor = new Panel
            {
                Size = new Size(tamanho.Width, 4),
                Location = new Point(0, 0),
                BackColor = AppTheme.CorPrimaria
            };

            Label lblTit = new Label
            {
                Text = titulo,
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = AppTheme.CorTextoPrincipal,
                Location = new Point(15, 18),
                AutoSize = true
            };

            Label lblDesc = new Label
            {
                Text = descricao,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = AppTheme.CorTextoSecundario,
                Location = new Point(15, 50),
                AutoSize = true
            };

            card.Controls.Add(barraCor);
            card.Controls.Add(lblTit);
            card.Controls.Add(lblDesc);

            EventHandler clickHandler = (s, ev) => abrirTela();
            card.Click += clickHandler;
            foreach (Control child in card.Controls)
            {
                child.Click += clickHandler;
            }

            card.MouseEnter += (s, ev) => card.BackColor = Color.FromArgb(245, 240, 220);
            card.MouseLeave += (s, ev) => card.BackColor = Color.FromArgb(250, 250, 250);

            return card;
        }

        private void CarregarLogo()
        {
            string[] nomes = { "logo-circular.jpg", "logo.jpg" };
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
                        {
                            miniLogo.Image = Image.FromFile(caminho);
                            return;
                        }
                    }
                }

                atual = atual.Parent;
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            AppSession.EncerrarSessao();
            Close();
        }
    }
}
