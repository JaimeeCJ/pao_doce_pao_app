# Plano de Estudos — Projeto Controle de Estoque

## Visão Geral do Projeto

**Objetivo:** Sistema de controle de estoque simples com login, cadastro de produtos, movimentação (entrada/saída) e relatórios.

### Stack Tecnológica

| Camada       | Tecnologia                        | Versão Recomendada          |
| ------------ | --------------------------------- | --------------------------- |
| Linguagem    | C#                                | .NET Framework 4.8          |
| Interface    | Windows Forms (WinForms)          | Nativo do .NET Framework    |
| Banco        | MySQL                             | 8.0 (Community Edition)     |
| Conector     | MySql.Data (NuGet)                | 8.0.x (última estável)      |
| IDE          | Visual Studio Community           | 2022                        |
| BD Client    | MySQL Workbench                   | 8.0                         |
| Relatórios   | ReportViewer (RDLC)               | Microsoft.ReportingServices.ReportViewerControl.WinForms |

> **Por que .NET Framework 4.8 e não .NET 8?** Para iniciantes, WinForms no Framework é mais estável, tem mais material em português, e o designer visual funciona perfeitamente no Visual Studio 2022. Menos atrito para quem está começando.

---

## Telas do Projeto

1. **Login** — autenticação simples (usuário + senha)
2. **Menu Principal** — navegação para as demais telas
3. **Cadastro de Produtos** — CRUD completo (criar, listar, editar, excluir)
4. **Cadastro de Categorias** — tabela auxiliar para organizar produtos
5. **Movimentação de Estoque** — registrar entradas e saídas com motivo
6. **Relatório de Estoque Atual** — posição consolidada
7. **Relatório de Movimentações** — filtro por período e produto

### Dica de UI/UX para WinForms

- Usar **MaterialSkin2** (NuGet: `MaterialSkin2DotNet`) para dar um visual moderno ao WinForms sem esforço extra. Traz componentes no estilo Material Design (botões, inputs, cards, cores) prontos para uso.
- Paleta de cores sugerida: Azul (`Primary`) + Cinza (`Dark`) + Branco (fundo).
- Usar `DataGridView` com estilo customizado para as listagens.
- Ícones gratuitos: **Material Design Icons** ou **Flaticon**.

---

## Modelo de Banco de Dados (MySQL)

```sql
CREATE DATABASE db_estoque;
USE db_estoque;

CREATE TABLE usuarios (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    login VARCHAR(50) NOT NULL UNIQUE,
    senha VARCHAR(255) NOT NULL,
    ativo TINYINT(1) DEFAULT 1,
    criado_em DATETIME DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE categorias (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    ativo TINYINT(1) DEFAULT 1
);

CREATE TABLE produtos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(150) NOT NULL,
    descricao TEXT,
    categoria_id INT,
    preco_unitario DECIMAL(10,2) DEFAULT 0,
    quantidade_atual INT DEFAULT 0,
    estoque_minimo INT DEFAULT 0,
    ativo TINYINT(1) DEFAULT 1,
    criado_em DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (categoria_id) REFERENCES categorias(id)
);

CREATE TABLE movimentacoes (
    id INT AUTO_INCREMENT PRIMARY KEY,
    produto_id INT NOT NULL,
    tipo ENUM('ENTRADA', 'SAIDA') NOT NULL,
    quantidade INT NOT NULL,
    motivo VARCHAR(255),
    usuario_id INT,
    criado_em DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (produto_id) REFERENCES produtos(id),
    FOREIGN KEY (usuario_id) REFERENCES usuarios(id)
);
```

---

## Módulos de Estudo (Roteiro por Tópicos)

### Módulo 1 — Fundamentos de C# (1ª semana)

Objetivo: entender a base da linguagem antes de mexer em tela.

- [ ] **Variáveis e tipos de dados** — `int`, `string`, `decimal`, `bool`, `DateTime`
- [ ] **Operadores** — aritméticos, lógicos, comparação
- [ ] **Estruturas de controle** — `if/else`, `switch`
- [ ] **Laços de repetição** — `for`, `foreach`, `while`
- [ ] **Métodos (funções)** — criar, parâmetros, retorno
- [ ] **Tratamento de erros** — `try/catch/finally`
- [ ] **Coleções básicas** — `List<T>`, arrays
- [ ] **Introdução a POO** — classes, objetos, propriedades, construtores
- [ ] **Modificadores de acesso** — `public`, `private`, `protected`

**Exercícios sugeridos:**
1. Calculadora de console (soma, subtração, multiplicação, divisão)
2. Cadastro de nomes em lista com menu de console (adicionar, listar, remover)
3. Criar classe `Produto` com propriedades e método para exibir informações

---

### Módulo 2 — Windows Forms Básico (2ª semana)

Objetivo: aprender a montar interfaces gráficas e eventos.

- [ ] **Criar um projeto WinForms** no Visual Studio
- [ ] **Toolbox — controles essenciais:**
  - `Label`, `TextBox`, `Button`, `ComboBox`
  - `DataGridView`, `DateTimePicker`, `GroupBox`, `Panel`
  - `MenuStrip`, `StatusStrip`
- [ ] **Propriedades dos controles** — Name, Text, Size, Anchor, Dock, Enabled, Visible
- [ ] **Eventos** — `Click`, `Load`, `TextChanged`, `CellClick`
- [ ] **Navegação entre formulários** — abrir/fechar forms, MDI ou ShowDialog
- [ ] **Validação de campos** — verificar campos vazios, numéricos, etc.
- [ ] **MessageBox** — alertas, confirmações
- [ ] **Instalando pacotes NuGet** — como buscar e instalar o MaterialSkin2

**Exercícios sugeridos:**
1. Tela de login com validação (campos obrigatórios)
2. Formulário de cadastro de pessoa (nome, idade, telefone) exibindo em DataGridView
3. Aplicar MaterialSkin2 no formulário (trocar cores, usar botões estilizados)

---

### Módulo 3 — Banco de Dados MySQL com C# (3ª semana)

Objetivo: conectar a aplicação ao banco e executar operações CRUD.

- [ ] **Instalar e configurar o MySQL** localmente
- [ ] **MySQL Workbench** — criar banco, tabelas, inserir dados de teste
- [ ] **Instalar MySql.Data via NuGet**
- [ ] **String de conexão** — montar e testar
  ```csharp
  string connStr = "Server=localhost;Database=db_estoque;Uid=root;Pwd=senha;";
  ```
- [ ] **MySqlConnection** — abrir e fechar conexão
- [ ] **MySqlCommand** — executar queries (INSERT, UPDATE, DELETE)
- [ ] **MySqlDataReader** — ler dados (SELECT)
- [ ] **Parâmetros (@parametro)** — evitar SQL Injection
  ```csharp
  cmd.Parameters.AddWithValue("@nome", txtNome.Text);
  ```
- [ ] **MySqlDataAdapter + DataTable** — preencher DataGridView facilmente
- [ ] **Classe de conexão centralizada** — criar uma classe `Conexao.cs` reutilizável

**Exercícios sugeridos:**
1. Inserir um produto no banco via formulário
2. Listar produtos do banco no DataGridView
3. Editar e excluir produto selecionado no grid
4. Criar a classe `Conexao.cs` que retorna `MySqlConnection`

---

### Módulo 4 — Arquitetura Simples do Projeto (4ª semana)

Objetivo: organizar o código em camadas mínimas para não virar bagunça.

- [ ] **Estrutura de pastas do projeto:**
  ```
  ControleEstoque/
  ├── Models/          → Classes de modelo (Produto, Usuario, Categoria, Movimentacao)
  ├── DAL/             → Data Access Layer (classes que falam com o banco)
  ├── BLL/             → Business Logic Layer (regras de negócio)
  ├── Views/           → Formulários WinForms
  ├── Utils/           → Helpers (conexão, validações, criptografia de senha)
  └── Reports/         → Arquivos RDLC de relatórios
  ```
- [ ] **Model** — classe C# espelhando a tabela do banco
- [ ] **DAL** — métodos: `Inserir()`, `Listar()`, `Atualizar()`, `Excluir()`, `BuscarPorId()`
- [ ] **BLL** — validações de negócio (ex: não permitir saída maior que estoque)
- [ ] **Separação de responsabilidades** — Form não acessa banco direto

**Exercício sugerido:**
1. Refatorar o CRUD de produtos do Módulo 3 usando Models + DAL + BLL

---

### Módulo 5 — Funcionalidades do Sistema (5ª semana)

Objetivo: implementar as features reais do projeto.

- [ ] **Tela de Login**
  - Validar usuário e senha no banco
  - Hash de senha com `SHA256` ou `BCrypt` (simplificado: SHA256)
  - Redirecionar para Menu Principal após login
- [ ] **Menu Principal**
  - Botões/menu para navegar entre telas
  - Exibir nome do usuário logado
- [ ] **CRUD de Categorias**
  - Cadastro simples com listagem
- [ ] **CRUD de Produtos**
  - ComboBox de categoria (carregada do banco)
  - Listagem com filtro por nome/categoria
  - Exibir alerta visual quando estoque abaixo do mínimo
- [ ] **Movimentação de Estoque**
  - Selecionar produto, tipo (Entrada/Saída), quantidade, motivo
  - Ao salvar: inserir na tabela `movimentacoes` e atualizar `quantidade_atual` do produto
  - Validar: saída não pode exceder estoque atual
- [ ] **Relatório de Estoque Atual**
  - DataGridView com todos produtos e quantidades
  - Destacar em vermelho itens abaixo do estoque mínimo
  - Botão para exportar/imprimir (RDLC ou simples export para Excel/CSV)
- [ ] **Relatório de Movimentações**
  - Filtro por data (de/até) e produto
  - Exibir em grid e opção de impressão

---

### Módulo 6 — Relatórios com RDLC (complementar)

Objetivo: gerar relatórios visuais para impressão.

- [ ] **Instalar ReportViewer** via NuGet
- [ ] **Criar arquivo .rdlc** no Visual Studio
- [ ] **Configurar DataSource** — DataTable como fonte de dados
- [ ] **Desenhar layout** — cabeçalho, tabela, rodapé com data/página
- [ ] **Exibir no ReportViewer** dentro de um Form
- [ ] **Exportar para PDF** programaticamente

> **Alternativa mais simples:** se RDLC for muito complexo para o prazo, exportar para CSV usando `StringBuilder` e `File.WriteAllText()`.

---

## Ferramentas para Instalar

| Ferramenta                    | Link                                                       |
| ----------------------------- | ---------------------------------------------------------- |
| Visual Studio Community 2022  | https://visualstudio.microsoft.com/pt-br/downloads/        |
| MySQL Community Server 8.0    | https://dev.mysql.com/downloads/mysql/                     |
| MySQL Workbench 8.0           | https://dev.mysql.com/downloads/workbench/                 |
| Git (opcional, recomendado)   | https://git-scm.com/downloads                              |

**Workload do Visual Studio:** na instalação, marcar **"Desenvolvimento para desktop com .NET"**.

---

## Materiais de Apoio Recomendados

### Canais YouTube (PT-BR)
- **CFBCursos** — Curso completo de C# (focado em iniciantes)
- **Hashtag Programação** — C# e conceitos gerais
- **Bóson Treinamentos** — MySQL do zero

### Documentação
- [Microsoft Learn — C#](https://learn.microsoft.com/pt-br/dotnet/csharp/)
- [MySQL 8.0 Reference Manual](https://dev.mysql.com/doc/refman/8.0/en/)
- [MaterialSkin2 GitHub](https://github.com/leocb/MaterialSkin)

---

## Cronograma Sugerido (5–6 semanas)

| Semana | Foco                                    | Entregável                              |
| ------ | --------------------------------------- | --------------------------------------- |
| 1      | Fundamentos C# (console)               | Exercícios de console funcionando       |
| 2      | WinForms + MaterialSkin2               | Telas de login e cadastro (sem banco)   |
| 3      | MySQL + conexão C#                     | CRUD de produtos conectado ao banco     |
| 4      | Arquitetura + refatoração              | Código organizado em camadas            |
| 5      | Features completas + movimentação      | Sistema funcional com todas as telas    |
| 6      | Relatórios + ajustes finais + testes   | Projeto finalizado e apresentável       |

---

## Dicas para o Mentor (Você)

1. **Code review semanal** — reserve 30min para revisar o código de cada um e dar feedback
2. **Pair programming** — nos primeiros dias, programe junto para destravar
3. **Template inicial** — entregue o projeto já com a estrutura de pastas e a classe de conexão pronta
4. **Banco já criado** — forneça o script SQL pronto para eles só executarem
5. **Git desde o dia 1** — mesmo que básico (`add`, `commit`, `push`), cria o hábito
6. **Erros são aprendizado** — não resolva tudo por eles, guie com perguntas
