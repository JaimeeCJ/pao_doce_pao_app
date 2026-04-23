using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsPaoDoce.Core;

namespace WindowsFormsPaoDoce.Views
{
    /// <summary>
    /// Form de exemplo mostrando como montar um CRUD basico.
    /// Use como referencia e depois delete este arquivo.
    /// Acesse pelo Menu Principal > botao "Exemplo".
    /// </summary>
    public partial class FormExemplo : Form
    {
        private long _idSelecionado = 0;
        private DataTable _dados;

        public FormExemplo()
        {
            InitializeComponent();
            CarregarDadosExemplo();
        }

        private void CarregarDadosExemplo()
        {
            _dados = new DataTable();
            _dados.Columns.Add("Id", typeof(long));
            _dados.Columns.Add("Nome", typeof(string));
            _dados.Columns.Add("Descricao", typeof(string));
            _dados.Columns.Add("Ativo", typeof(string));

            _dados.Rows.Add(1, "Item A", "Primeiro item de exemplo", "Sim");
            _dados.Rows.Add(2, "Item B", "Segundo item de exemplo", "Sim");
            _dados.Rows.Add(3, "Item C", "Terceiro item de exemplo", "Nao");

            gridDados.DataSource = _dados;
        }

        private void gridDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = gridDados.Rows[e.RowIndex];
            _idSelecionado = Convert.ToInt64(row.Cells["Id"].Value);
            txtNome.Text = row.Cells["Nome"].Value.ToString();
            txtDescricao.Text = row.Cells["Descricao"].Value.ToString();
            chkAtivo.Checked = row.Cells["Ativo"].Value.ToString() == "Sim";

            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Informe o nome.", "Validacao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            long novoId = _dados.Rows.Count + 1;
            _dados.Rows.Add(novoId, txtNome.Text.Trim(), txtDescricao.Text.Trim(), chkAtivo.Checked ? "Sim" : "Nao");
            LimparCampos();

            // No projeto real, aqui voce faria:
            // string sql = "INSERT INTO tabela (nome, descricao, ativo) VALUES (@nome, @desc, @ativo)";
            // usando MySqlCommand com Parameters.AddWithValue
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (_idSelecionado <= 0) return;

            foreach (DataRow row in _dados.Rows)
            {
                if (Convert.ToInt64(row["Id"]) == _idSelecionado)
                {
                    row["Nome"] = txtNome.Text.Trim();
                    row["Descricao"] = txtDescricao.Text.Trim();
                    row["Ativo"] = chkAtivo.Checked ? "Sim" : "Nao";
                    break;
                }
            }

            LimparCampos();

            // No projeto real, aqui voce faria:
            // string sql = "UPDATE tabela SET nome=@nome, descricao=@desc, ativo=@ativo WHERE id=@id";
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (_idSelecionado <= 0) return;

            DialogResult confirma = MessageBox.Show("Deseja excluir?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirma != DialogResult.Yes) return;

            for (int i = _dados.Rows.Count - 1; i >= 0; i--)
            {
                if (Convert.ToInt64(_dados.Rows[i]["Id"]) == _idSelecionado)
                {
                    _dados.Rows.RemoveAt(i);
                    break;
                }
            }

            LimparCampos();

            // No projeto real, aqui voce faria:
            // string sql = "DELETE FROM tabela WHERE id=@id";
            // ou UPDATE tabela SET ativo=0 WHERE id=@id (soft delete)
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LimparCampos()
        {
            _idSelecionado = 0;
            txtNome.Clear();
            txtDescricao.Clear();
            chkAtivo.Checked = true;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        private void gridDados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
