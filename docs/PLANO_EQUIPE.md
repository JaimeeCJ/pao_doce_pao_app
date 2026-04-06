# Plano de Execucao do Grupo

## Objetivo

Permitir que os 5 integrantes desenvolvam em paralelo com o minimo de bloqueio entre telas.

## Ordem recomendada (para reduzir dependencia)

1. **Artur - Cadastro de Categorias**
   - Entregar CRUD de categorias primeiro
   - Produto depende de categorias para ComboBox
2. **Diogo - Cadastro de Produtos**
   - CRUD completo com categoria obrigatoria
   - Movimentacao depende de produto
3. **Bruno - Login**
   - Substituir autenticacao fake por banco (`usuarios`)
4. **Jaime - Movimentacao de Estoque**
   - Entrada/Saida atualizando `produtos.quantidade_atual`
5. **Allan - Relatorio de Estoque Atual**
   - Depende de produtos + movimentacoes corretas
6. **Flavio - Menu Principal**
   - Ajustes finais de navegacao e permissao por perfil (opcional)

## Contratos entre modulos

- Categorias deve disponibilizar:
  - Listagem ativa para preencher ComboBox de produtos
- Produtos deve disponibilizar:
  - Listagem de produtos ativos para movimentacao
  - Quantidade atual e estoque minimo
- Movimentacao deve garantir:
  - Nao permitir saida maior que estoque
  - Atualizar saldo do produto na mesma transacao logica
- Relatorio deve usar:
  - Dados consolidados da tabela `produtos`

## Regras de branch e merge

- Cada pessoa trabalha na propria branch:
  - `feature/bruno-login`
  - `feature/flavio-menu`
  - `feature/diogo-produtos`
  - `feature/artur-categorias`
  - `feature/jaime-movimentacao`
  - `feature/allan-relatorio-estoque`
- PR pequeno, com descricao objetiva e print da tela
- Merge apenas com build passando

## Entregaveis minimos por tela

- Login:
  - Validacao no banco (`login` + `senha_hash`)
- Menu:
  - Navegacao para todas as telas
- Categorias:
  - Inserir, editar, listar, inativar
- Produtos:
  - Inserir, editar, listar, inativar e filtro por nome/categoria
- Movimentacao:
  - Entrada/Saida com motivo e validacao de estoque
- Relatorio estoque:
  - Grade consolidada e destaque para itens abaixo do minimo
