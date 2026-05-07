namespace WindowsFormsPaoDoce.Views
{
    partial class MovimentacaoEstoqueForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlCabecalho = new System.Windows.Forms.Panel();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.grpRegistrar = new System.Windows.Forms.GroupBox();
            this.lblProduto = new System.Windows.Forms.Label();
            this.cmbProduto = new System.Windows.Forms.ComboBox();
            this.lblEstoqueAtual = new System.Windows.Forms.Label();
            this.txtEstoqueAtual = new System.Windows.Forms.TextBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.rbEntrada = new System.Windows.Forms.RadioButton();
            this.rbSaida = new System.Windows.Forms.RadioButton();
            this.lblQuantidade = new System.Windows.Forms.Label();
            this.txtQuantidade = new System.Windows.Forms.TextBox();
            this.lblMotivo = new System.Windows.Forms.Label();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.grpHistorico = new System.Windows.Forms.GroupBox();
            this.lblBusca = new System.Windows.Forms.Label();
            this.txtBusca = new System.Windows.Forms.TextBox();
            this.dgvMovimentacoes = new System.Windows.Forms.DataGridView();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.pnlCabecalho.SuspendLayout();
            this.grpRegistrar.SuspendLayout();
            this.grpHistorico.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimentacoes)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCabecalho
            // 
            this.pnlCabecalho.BackColor = System.Drawing.Color.FromArgb(219, 181, 95);
            this.pnlCabecalho.Controls.Add(this.lblSubtitulo);
            this.pnlCabecalho.Controls.Add(this.lblTitulo);
            this.pnlCabecalho.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCabecalho.Location = new System.Drawing.Point(0, 0);
            this.pnlCabecalho.Name = "pnlCabecalho";
            this.pnlCabecalho.Size = new System.Drawing.Size(964, 80);
            this.pnlCabecalho.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.Black;
            this.lblTitulo.Location = new System.Drawing.Point(20, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(310, 30);
            this.lblTitulo.Text = "Movimentacao de Estoque";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblSubtitulo.Location = new System.Drawing.Point(24, 48);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(280, 17);
            this.lblSubtitulo.Text = "Registrar entradas e saidas de produtos no estoque";
            // 
            // grpRegistrar
            // 
            this.grpRegistrar.Controls.Add(this.lblProduto);
            this.grpRegistrar.Controls.Add(this.cmbProduto);
            this.grpRegistrar.Controls.Add(this.lblEstoqueAtual);
            this.grpRegistrar.Controls.Add(this.txtEstoqueAtual);
            this.grpRegistrar.Controls.Add(this.lblTipo);
            this.grpRegistrar.Controls.Add(this.rbEntrada);
            this.grpRegistrar.Controls.Add(this.rbSaida);
            this.grpRegistrar.Controls.Add(this.lblQuantidade);
            this.grpRegistrar.Controls.Add(this.txtQuantidade);
            this.grpRegistrar.Controls.Add(this.lblMotivo);
            this.grpRegistrar.Controls.Add(this.txtMotivo);
            this.grpRegistrar.Controls.Add(this.btnRegistrar);
            this.grpRegistrar.Controls.Add(this.btnLimpar);
            this.grpRegistrar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpRegistrar.Location = new System.Drawing.Point(20, 95);
            this.grpRegistrar.Name = "grpRegistrar";
            this.grpRegistrar.Size = new System.Drawing.Size(924, 200);
            this.grpRegistrar.TabIndex = 1;
            this.grpRegistrar.TabStop = false;
            this.grpRegistrar.Text = "Registrar Movimentacao";
            // 
            // lblProduto
            // 
            this.lblProduto.AutoSize = true;
            this.lblProduto.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblProduto.Location = new System.Drawing.Point(15, 35);
            this.lblProduto.Name = "lblProduto";
            this.lblProduto.Text = "Produto:";
            // 
            // cmbProduto
            // 
            this.cmbProduto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduto.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cmbProduto.Location = new System.Drawing.Point(15, 58);
            this.cmbProduto.Name = "cmbProduto";
            this.cmbProduto.Size = new System.Drawing.Size(340, 25);
            this.cmbProduto.TabIndex = 0;
            // 
            // lblEstoqueAtual
            // 
            this.lblEstoqueAtual.AutoSize = true;
            this.lblEstoqueAtual.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblEstoqueAtual.Location = new System.Drawing.Point(370, 35);
            this.lblEstoqueAtual.Name = "lblEstoqueAtual";
            this.lblEstoqueAtual.Text = "Estoque Atual:";
            // 
            // txtEstoqueAtual
            // 
            this.txtEstoqueAtual.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtEstoqueAtual.Location = new System.Drawing.Point(370, 58);
            this.txtEstoqueAtual.Name = "txtEstoqueAtual";
            this.txtEstoqueAtual.ReadOnly = true;
            this.txtEstoqueAtual.Size = new System.Drawing.Size(100, 25);
            this.txtEstoqueAtual.TabIndex = 1;
            this.txtEstoqueAtual.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTipo.Location = new System.Drawing.Point(500, 35);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Text = "Tipo:";
            // 
            // rbEntrada
            // 
            this.rbEntrada.AutoSize = true;
            this.rbEntrada.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.rbEntrada.Location = new System.Drawing.Point(500, 58);
            this.rbEntrada.Name = "rbEntrada";
            this.rbEntrada.Text = "Entrada";
            this.rbEntrada.Checked = true;
            this.rbEntrada.TabIndex = 2;
            // 
            // rbSaida
            // 
            this.rbSaida.AutoSize = true;
            this.rbSaida.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.rbSaida.Location = new System.Drawing.Point(600, 58);
            this.rbSaida.Name = "rbSaida";
            this.rbSaida.Text = "Saida";
            this.rbSaida.TabIndex = 3;
            // 
            // lblQuantidade
            // 
            this.lblQuantidade.AutoSize = true;
            this.lblQuantidade.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblQuantidade.Location = new System.Drawing.Point(700, 35);
            this.lblQuantidade.Name = "lblQuantidade";
            this.lblQuantidade.Text = "Quantidade:";
            // 
            // txtQuantidade
            // 
            this.txtQuantidade.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtQuantidade.Location = new System.Drawing.Point(700, 58);
            this.txtQuantidade.Name = "txtQuantidade";
            this.txtQuantidade.Size = new System.Drawing.Size(100, 25);
            this.txtQuantidade.TabIndex = 4;
            // 
            // lblMotivo
            // 
            this.lblMotivo.AutoSize = true;
            this.lblMotivo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblMotivo.Location = new System.Drawing.Point(15, 100);
            this.lblMotivo.Name = "lblMotivo";
            this.lblMotivo.Text = "Motivo:";
            // 
            // txtMotivo
            // 
            this.txtMotivo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtMotivo.Location = new System.Drawing.Point(15, 123);
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.Size = new System.Drawing.Size(580, 25);
            this.txtMotivo.TabIndex = 5;
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.BackColor = System.Drawing.Color.FromArgb(219, 181, 95);
            this.btnRegistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnRegistrar.Location = new System.Drawing.Point(620, 118);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(140, 36);
            this.btnRegistrar.TabIndex = 6;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.UseVisualStyleBackColor = false;
            this.btnRegistrar.Cursor = System.Windows.Forms.Cursors.Hand;
            // 
            // btnLimpar
            // 
            this.btnLimpar.BackColor = System.Drawing.Color.White;
            this.btnLimpar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLimpar.Location = new System.Drawing.Point(775, 118);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(130, 36);
            this.btnLimpar.TabIndex = 7;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseVisualStyleBackColor = false;
            this.btnLimpar.Cursor = System.Windows.Forms.Cursors.Hand;
            // 
            // grpHistorico
            // 
            this.grpHistorico.Controls.Add(this.lblBusca);
            this.grpHistorico.Controls.Add(this.txtBusca);
            this.grpHistorico.Controls.Add(this.dgvMovimentacoes);
            this.grpHistorico.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpHistorico.Location = new System.Drawing.Point(20, 310);
            this.grpHistorico.Name = "grpHistorico";
            this.grpHistorico.Size = new System.Drawing.Size(924, 270);
            this.grpHistorico.TabIndex = 2;
            this.grpHistorico.TabStop = false;
            this.grpHistorico.Text = "Historico de Movimentacoes";
            // 
            // lblBusca
            // 
            this.lblBusca.AutoSize = true;
            this.lblBusca.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblBusca.Location = new System.Drawing.Point(15, 28);
            this.lblBusca.Name = "lblBusca";
            this.lblBusca.Text = "Filtrar por produto:";
            // 
            // txtBusca
            // 
            this.txtBusca.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtBusca.Location = new System.Drawing.Point(145, 25);
            this.txtBusca.Name = "txtBusca";
            this.txtBusca.Size = new System.Drawing.Size(300, 25);
            this.txtBusca.TabIndex = 8;
            // 
            // dgvMovimentacoes
            // 
            this.dgvMovimentacoes.Location = new System.Drawing.Point(15, 60);
            this.dgvMovimentacoes.Name = "dgvMovimentacoes";
            this.dgvMovimentacoes.Size = new System.Drawing.Size(893, 195);
            this.dgvMovimentacoes.TabIndex = 9;
            this.dgvMovimentacoes.ReadOnly = true;
            this.dgvMovimentacoes.AllowUserToAddRows = false;
            this.dgvMovimentacoes.AllowUserToDeleteRows = false;
            this.dgvMovimentacoes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMovimentacoes.MultiSelect = false;
            this.dgvMovimentacoes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMovimentacoes.RowHeadersVisible = false;
            this.dgvMovimentacoes.BackgroundColor = System.Drawing.Color.White;
            this.dgvMovimentacoes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvMovimentacoes.EnableHeadersVisualStyles = false;
            this.dgvMovimentacoes.ColumnHeadersHeight = 36;
            this.dgvMovimentacoes.RowTemplate.Height = 30;
            // 
            // btnVoltar
            // 
            this.btnVoltar.BackColor = System.Drawing.Color.White;
            this.btnVoltar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVoltar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnVoltar.Location = new System.Drawing.Point(20, 593);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(120, 36);
            this.btnVoltar.TabIndex = 10;
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.UseVisualStyleBackColor = false;
            this.btnVoltar.Cursor = System.Windows.Forms.Cursors.Hand;
            // 
            // MovimentacaoEstoqueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(964, 641);
            this.Controls.Add(this.pnlCabecalho);
            this.Controls.Add(this.grpRegistrar);
            this.Controls.Add(this.grpHistorico);
            this.Controls.Add(this.btnVoltar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MovimentacaoEstoqueForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Movimentacao de Estoque";
            this.Load += new System.EventHandler(this.MovimentacaoEstoqueForm_Load);
            this.pnlCabecalho.ResumeLayout(false);
            this.pnlCabecalho.PerformLayout();
            this.grpRegistrar.ResumeLayout(false);
            this.grpRegistrar.PerformLayout();
            this.grpHistorico.ResumeLayout(false);
            this.grpHistorico.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimentacoes)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlCabecalho;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.GroupBox grpRegistrar;
        private System.Windows.Forms.Label lblProduto;
        private System.Windows.Forms.ComboBox cmbProduto;
        private System.Windows.Forms.Label lblEstoqueAtual;
        private System.Windows.Forms.TextBox txtEstoqueAtual;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.RadioButton rbEntrada;
        private System.Windows.Forms.RadioButton rbSaida;
        private System.Windows.Forms.Label lblQuantidade;
        private System.Windows.Forms.TextBox txtQuantidade;
        private System.Windows.Forms.Label lblMotivo;
        private System.Windows.Forms.TextBox txtMotivo;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.GroupBox grpHistorico;
        private System.Windows.Forms.Label lblBusca;
        private System.Windows.Forms.TextBox txtBusca;
        private System.Windows.Forms.DataGridView dgvMovimentacoes;
        private System.Windows.Forms.Button btnVoltar;
    }
}
