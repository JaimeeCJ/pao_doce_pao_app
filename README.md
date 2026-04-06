# Pao Doce Pao - Controle de Estoque

Sistema de controle de estoque para a Panificadora e Confeitaria Pao Doce Pao.
Projeto academico desenvolvido em grupo (5 integrantes).

## Stack

| Camada    | Tecnologia            |
|-----------|-----------------------|
| Linguagem | C# (.NET Framework 4.7.2) |
| Interface | Windows Forms         |
| Banco     | MySQL 8.0             |
| Conector  | MySql.Data (NuGet)    |
| IDE       | Visual Studio 2022    |

## Estrutura do Projeto

```
WindowsFormsPaoDoce/
├── assets/                          # Logo da padaria
│   ├── logo.jpg
│   └── logo-circular.jpg
├── database/
│   └── 01_schema.sql                # Script de criacao do banco
├── docs/
│   └── PLANO_EQUIPE.md              # Divisao de tarefas e dependencias
├── WindowsFormsPaoDoce/             # Projeto WinForms
│   ├── Core/
│   │   ├── AppSession.cs            # Sessao do usuario logado
│   │   └── AppTheme.cs              # Cores e helpers visuais
│   ├── Models/
│   │   ├── Categoria.cs
│   │   ├── Movimentacao.cs
│   │   ├── Produto.cs
│   │   ├── TipoMovimentacao.cs
│   │   └── Usuario.cs
│   ├── Services/
│   │   └── AuthService.cs           # Autenticacao (fake, trocar por banco)
│   ├── Views/
│   │   ├── LoginForm.cs             # Tela de login
│   │   ├── MainMenuForm.cs          # Menu principal com navegacao
│   │   ├── CategoriasForm.cs        # Em branco — Artur
│   │   ├── ProdutosForm.cs          # Em branco — Diogo
│   │   ├── MovimentacaoEstoqueForm.cs # Em branco — Jaime
│   │   ├── RelatorioEstoqueForm.cs  # Em branco — Allan
│   │   └── FormExemplo.cs           # CRUD de referencia (deletar depois)
│   ├── Program.cs
│   ├── App.config                   # Connection string MySQL
│   └── WindowsFormsPaoDoce.csproj
├── WindowsFormsPaoDoce.slnx         # Solution
├── .gitignore
└── README.md
```

## Divisao do Grupo

| Tela                       | Responsavel | Arquivo                          |
|----------------------------|-------------|----------------------------------|
| Login                      | Bruno       | Views/LoginForm.cs               |
| Menu Principal             | Flavio      | Views/MainMenuForm.cs            |
| Cadastro de Categorias     | Artur       | Views/CategoriasForm.cs          |
| Cadastro de Produtos       | Diogo       | Views/ProdutosForm.cs            |
| Movimentacao de Estoque    | Jaime       | Views/MovimentacaoEstoqueForm.cs |
| Relatorio de Estoque Atual | Allan       | Views/RelatorioEstoqueForm.cs    |

## Ordem Recomendada de Desenvolvimento

1. **Artur** — Categorias (nao depende de nada)
2. **Diogo** — Produtos (depende de Categorias para ComboBox)
3. **Bruno** — Login com banco (trocar o AuthService fake)
4. **Jaime** — Movimentacao (depende de Produtos + Login)
5. **Allan** — Relatorio (depende de Produtos + Movimentacoes)
6. **Flavio** — Menu (ajustes finais de navegacao)

## Como Rodar

1. Abrir `WindowsFormsPaoDoce.slnx` no Visual Studio 2022
2. Compilar (Ctrl+Shift+B)
3. Executar (F5)
4. Login: `admin` / `admin123`

## Banco de Dados

1. Ajustar a connection string em `App.config` (usuario e senha do seu MySQL)

## FormExemplo

Ha um form de exemplo (`Views/FormExemplo.cs`) com CRUD completo funcionando em memoria.
Serve de referencia para o grupo aprender o padrao. Pode ser deletado quando nao precisar mais.

## Branches por Pessoa

```
feature/bruno-login
feature/flavio-menu
feature/diogo-produtos
feature/artur-categorias
feature/jaime-movimentacao
feature/allan-relatorio-estoque
```
