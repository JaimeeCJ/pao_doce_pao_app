using MySql.Data.MySqlClient;
using System;
using System.Data;
using WindowsFormsPaoDoce.Models;

namespace WindowsFormsPaoDoce.Services
{
    public class MovimentacaoService
    {
        private readonly string _conexao =
            "Server=62.171.167.217;Port=33060;Database=db_padaria;Uid=root_padaria;Pwd=Pad2026SecPad;";

        public DataTable ListarProdutosAtivos()
        {
            using (MySqlConnection conn = new MySqlConnection(_conexao))
            {
                conn.Open();
                string sql = "SELECT id, nome, quantidade_atual, estoque_minimo FROM produtos WHERE ativo = 1 ORDER BY nome";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable ListarMovimentacoes()
        {
            using (MySqlConnection conn = new MySqlConnection(_conexao))
            {
                conn.Open();
                string sql = @"SELECT m.id,
                                      p.nome AS produto,
                                      m.tipo,
                                      m.quantidade,
                                      m.motivo,
                                      m.criado_em
                               FROM movimentacoes m
                               INNER JOIN produtos p ON p.id = m.produto_id
                               ORDER BY m.criado_em DESC";

                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable FiltrarMovimentacoes(string filtroNomeProduto)
        {
            using (MySqlConnection conn = new MySqlConnection(_conexao))
            {
                conn.Open();
                string sql = @"SELECT m.id,
                                      p.nome AS produto,
                                      m.tipo,
                                      m.quantidade,
                                      m.motivo,
                                      m.criado_em
                               FROM movimentacoes m
                               INNER JOIN produtos p ON p.id = m.produto_id
                               WHERE p.nome LIKE @filtro
                               ORDER BY m.criado_em DESC";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@filtro", "%" + filtroNomeProduto + "%");

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public int ObterEstoqueAtual(long produtoId)
        {
            using (MySqlConnection conn = new MySqlConnection(_conexao))
            {
                conn.Open();
                string sql = "SELECT quantidade_atual FROM produtos WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", produtoId);
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        public void RegistrarMovimentacao(Movimentacao mov)
        {
            using (MySqlConnection conn = new MySqlConnection(_conexao))
            {
                conn.Open();
                using (MySqlTransaction transacao = conn.BeginTransaction())
                {
                    try
                    {
                        string sqlInsert = @"INSERT INTO movimentacoes 
                                             (produto_id, tipo, quantidade, motivo, usuario_id, criado_em)
                                             VALUES (@produtoId, @tipo, @quantidade, @motivo, @usuarioId, NOW())";

                        MySqlCommand cmdInsert = new MySqlCommand(sqlInsert, conn, transacao);
                        cmdInsert.Parameters.AddWithValue("@produtoId", mov.ProdutoId);
                        cmdInsert.Parameters.AddWithValue("@tipo", mov.Tipo == TipoMovimentacao.ENTRADA ? "ENTRADA" : "SAIDA");
                        cmdInsert.Parameters.AddWithValue("@quantidade", mov.Quantidade);
                        cmdInsert.Parameters.AddWithValue("@motivo", string.IsNullOrWhiteSpace(mov.Motivo) ? (object)DBNull.Value : mov.Motivo);
                        cmdInsert.Parameters.AddWithValue("@usuarioId", mov.UsuarioId.HasValue ? (object)mov.UsuarioId.Value : DBNull.Value);
                        cmdInsert.ExecuteNonQuery();

                        string operacao = mov.Tipo == TipoMovimentacao.ENTRADA
                            ? "quantidade_atual + @qtd"
                            : "quantidade_atual - @qtd";

                        string sqlUpdate = "UPDATE produtos SET quantidade_atual = " + operacao + ", atualizado_em = NOW() WHERE id = @prodId";
                        MySqlCommand cmdUpdate = new MySqlCommand(sqlUpdate, conn, transacao);
                        cmdUpdate.Parameters.AddWithValue("@qtd", mov.Quantidade);
                        cmdUpdate.Parameters.AddWithValue("@prodId", mov.ProdutoId);
                        cmdUpdate.ExecuteNonQuery();

                        transacao.Commit();
                    }
                    catch
                    {
                        transacao.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
