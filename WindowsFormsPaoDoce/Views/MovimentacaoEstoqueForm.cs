using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsPaoDoce.Core;
using WindowsFormsPaoDoce.Models;
using WindowsFormsPaoDoce.Services;

namespace WindowsFormsPaoDoce.Views
{
    public partial class MovimentacaoEstoqueForm : Form
    {
        private readonly MovimentacaoService _service = new MovimentacaoService();

        public MovimentacaoEstoqueForm()
        {
            InitializeComponent();
        }

        private void MovimentacaoEstoqueForm_Load(object sender, EventArgs e)
        {
            CarregarProdutos();
            CarregarHistorico();
            EstilizarGrid();
            WireEvents();
        }

        private void WireEvents()
        {
            cmbProduto.SelectedIndexChanged += CmbProduto_SelectedIndexChanged;
            btnRegistrar.Click += BtnRegistrar_Click;
            btnLimpar.Click += BtnLimpar_Click;
            btnVoltar.Click += BtnVoltar_Click;
            txtBusca.TextChanged += TxtBusca_TextChanged;
        }

        private void CarregarProdutos()
        {
            try
            {
                DataTable dt = _service.ListarProdutosAtivos();
                cmbProduto.DataSource = dt;
                cmbProduto.DisplayMember = "nome";
                cmbProduto.ValueMember = "id";

                if (dt.Rows.Count > 0)
                    AtualizarEstoqueExibido();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar produtos: " + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarHistorico()
        {
            try
            {
                DataTable dt = _service.ListarMovimentacoes();
                dgvMovimentacoes.DataSource = dt;
                FormatarColunas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar historico: " + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatarColunas()
        {
            if (dgvMovimentacoes.Columns.Count == 0) return;

            dgvMovimentacoes.Columns["id"].HeaderText = "Codigo";
            dgvMovimentacoes.Columns["produto"].HeaderText = "Produto";
            dgvMovimentacoes.Columns["tipo"].HeaderText = "Tipo";
            dgvMovimentacoes.Columns["quantidade"].HeaderText = "Qtd";
            dgvMovimentacoes.Columns["motivo"].HeaderText = "Motivo";
            dgvMovimentacoes.Columns["criado_em"].HeaderText = "Data/Hora";
            dgvMovimentacoes.Columns["criado_em"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            dgvMovimentacoes.Columns["id"].Width = 60;
            dgvMovimentacoes.Columns["quantidade"].Width = 60;
        }

        private void EstilizarGrid()
        {
            dgvMovimentacoes.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvMovimentacoes.DefaultCellStyle.SelectionBackColor = AppTheme.CorPrimaria;
            dgvMovimentacoes.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvMovimentacoes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvMovimentacoes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            dgvMovimentacoes.CellFormatting += DgvMovimentacoes_CellFormatting;
        }

        private void DgvMovimentacoes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvMovimentacoes.Columns[e.ColumnIndex].Name == "tipo" && e.Value != null)
            {
                string tipo = e.Value.ToString();
                if (tipo == "ENTRADA")
                {
                    e.CellStyle.ForeColor = AppTheme.CorSucesso;
                    e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                }
                else if (tipo == "SAIDA")
                {
                    e.CellStyle.ForeColor = AppTheme.CorPerigo;
                    e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                }
            }
        }

        private void AtualizarEstoqueExibido()
        {
            if (cmbProduto.SelectedValue == null) return;

            try
            {
                long produtoId = Convert.ToInt64(cmbProduto.SelectedValue);
                int estoque = _service.ObterEstoqueAtual(produtoId);
                txtEstoqueAtual.Text = estoque.ToString();
            }
            catch
            {
                txtEstoqueAtual.Text = "0";
            }
        }

        private void CmbProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizarEstoqueExibido();
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            if (cmbProduto.SelectedValue == null)
            {
                MessageBox.Show("Selecione um produto.", "Validacao",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtQuantidade.Text, out int quantidade) || quantidade <= 0)
            {
                MessageBox.Show("Informe uma quantidade valida (maior que zero).", "Validacao",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantidade.Focus();
                return;
            }

            TipoMovimentacao tipo = rbEntrada.Checked
                ? TipoMovimentacao.ENTRADA
                : TipoMovimentacao.SAIDA;

            long produtoId = Convert.ToInt64(cmbProduto.SelectedValue);

            if (tipo == TipoMovimentacao.SAIDA)
            {
                int estoqueAtual = _service.ObterEstoqueAtual(produtoId);
                if (quantidade > estoqueAtual)
                {
                    MessageBox.Show(
                        "Quantidade de saida (" + quantidade + ") excede o estoque atual (" + estoqueAtual + ").",
                        "Estoque insuficiente",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string descricaoTipo = tipo == TipoMovimentacao.ENTRADA ? "ENTRADA" : "SAIDA";
            DialogResult confirma = MessageBox.Show(
                "Confirmar " + descricaoTipo + " de " + quantidade + " unidade(s)?",
                "Confirmar Movimentacao",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirma != DialogResult.Yes) return;

            try
            {
                Movimentacao mov = new Movimentacao
                {
                    ProdutoId = produtoId,
                    Tipo = tipo,
                    Quantidade = quantidade,
                    Motivo = txtMotivo.Text.Trim(),
                    UsuarioId = AppSession.UsuarioId
                };

                _service.RegistrarMovimentacao(mov);

                MessageBox.Show("Movimentacao registrada com sucesso!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimparCampos();
                AtualizarEstoqueExibido();
                CarregarHistorico();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao registrar: " + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void BtnVoltar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TxtBusca_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string filtro = txtBusca.Text.Trim();
                DataTable dt = string.IsNullOrEmpty(filtro)
                    ? _service.ListarMovimentacoes()
                    : _service.FiltrarMovimentacoes(filtro);

                dgvMovimentacoes.DataSource = dt;
                FormatarColunas();
            }
            catch
            {
            }
        }

        private void LimparCampos()
        {
            txtQuantidade.Clear();
            txtMotivo.Clear();
            rbEntrada.Checked = true;
            txtQuantidade.Focus();
        }
    }
}
