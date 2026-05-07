using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WindowsFormsPaoDoce.Core;

namespace WindowsFormsPaoDoce.Views
{
    // changed class declaration to partial to match other partial definition (designer file)
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            AppTheme.EstilizarFormularioPadrao(this);
            Text = "Pao Doce Pao - Menu Principal";
            Size = new Size(820, 600);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            Panel cabecalho = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = AppTheme.CorPrimaria
            };

            PictureBox miniLogo = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(80, 80),
                Location = new Point(14, 10)
            };
            CarregarLogo(miniLogo);

            Label lblTitulo = new Label
            {
                Text = "Pao Doce Pao",
                Font = new Font("Segoe UI", 22F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(100, 10),
                ForeColor = Color.Black
            };

            Label lblSubtitulo = new Label
            {
                Text = "Controle de Estoque — " + (AppSession.NomeUsuario ?? "N/A"),
                AutoSize = true,
                Font = new Font("Segoe UI", 11F),
                Location = new Point(104, 55),
                ForeColor = Color.FromArgb(50, 50, 50)
            };

            cabecalho.Controls.Add(miniLogo);
            cabecalho.Controls.Add(lblTitulo);
            cabecalho.Controls.Add(lblSubtitulo);

            Panel painelBotoes = new Panel
            {
                Location = new Point(30, 125),
                Size = new Size(740, 380)
            };

            int coluna1 = 0;
            int coluna2 = 380;
            int linha1 = 0;
            int linha2 = 130;
            int linha3 = 260;
            Size tamBotao = new Size(350, 110);

            painelBotoes.Controls.Add(CriarBotaoModulo(
                "Cadastro de Categorias", "Artur — Organizar produtos por tipo",
                tamBotao, new Point(coluna1, linha1), () => new CategoriasForm().ShowDialog()));

            painelBotoes.Controls.Add(CriarBotaoModulo(
                "Cadastro de Produtos", "Diogo — CRUD completo com categoria",
                tamBotao, new Point(coluna2, linha1), () => new ProdutosForm().ShowDialog()));

            painelBotoes.Controls.Add(CriarBotaoModulo(
                "Movimentacao de Estoque", "Jaime — Entradas e saidas com motivo",
                tamBotao, new Point(coluna1, linha2), () => new MovimentacaoEstoqueForm().ShowDialog()));

            painelBotoes.Controls.Add(CriarBotaoModulo(
                "Relatorio de Estoque", "Allan — Posicao consolidada",
                tamBotao, new Point(coluna2, linha2), () => new RelatorioEstoqueForm().ShowDialog()));

            painelBotoes.Controls.Add(CriarBotaoModulo(
                "Exemplo de CRUD", "Referencia para o grupo — pode deletar depois",
                tamBotao, new Point(coluna1, linha3), () => new FormExemplo().ShowDialog()));

            Panel painelRodape = new Panel
            {
                Location = new Point(coluna2, linha3),
                Size = new Size(350, 110)
            };

            Button btnSair = new Button
            {
                Text = "Encerrar Sessao",
                Size = new Size(180, 42),
                Location = new Point(170, 34),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSair.Click += BtnSair_Click;

            painelRodape.Controls.Add(btnSair);
            painelBotoes.Controls.Add(painelRodape);

            Controls.Add(cabecalho);
            Controls.Add(painelBotoes);
        }

        private static Control CriarBotaoModulo(string titulo, string descricao, Size tamanho, Point posicao, Action abrirTela)
        {
            Panel card = new Panel
            {
                Size = tamanho,
                Location = posicao,
                BackColor = Color.FromArgb(250, 250, 250),
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand
            };

            Label lblTitulo = new Label
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

            Panel barraCor = new Panel
            {
                Size = new Size(tamanho.Width, 4),
                Location = new Point(0, 0),
                BackColor = AppTheme.CorPrimaria
            };

            card.Controls.Add(barraCor);
            card.Controls.Add(lblTitulo);
            card.Controls.Add(lblDesc);

            EventHandler clickHandler = (s, ev) => abrirTela();
            card.Click += clickHandler;
            lblTitulo.Click += clickHandler;
            lblDesc.Click += clickHandler;

            card.MouseEnter += (s, ev) => card.BackColor = Color.FromArgb(245, 240, 220);
            card.MouseLeave += (s, ev) => card.BackColor = Color.FromArgb(250, 250, 250);

            return card;
        }

        private static void CarregarLogo(PictureBox picture)
        {
            string[] nomesArquivo =
            {
                "logo.jpg",
                "PHOTO-2026-03-12-09-22-54.jpg.jpeg",
                "PHOTO-2026-03-12-09-22-54 2.jpg.jpeg"
            };

            DirectoryInfo atual = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            for (int nivel = 0; nivel < 5 && atual != null; nivel++)
            {
                for (int i = 0; i < nomesArquivo.Length; i++)
                {
                    string candidato = Path.Combine(atual.FullName, nomesArquivo[i]);
                    if (File.Exists(candidato))
                    {
                        picture.Image = Image.FromFile(candidato);
                        return;
                    }
                }

                atual = atual.Parent;
            }
        }

        private void BtnSair_Click(object sender, EventArgs e)
        {
            AppSession.EncerrarSessao();
            Close();
        }
    }
}
