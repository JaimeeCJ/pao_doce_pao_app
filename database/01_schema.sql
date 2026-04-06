-- Schema padronizado para o projeto de controle de estoque
-- Compatibilidade alvo: MySQL 8.0+

CREATE DATABASE IF NOT EXISTS db_estoque
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;

USE db_estoque;

-- Ordem de drop para manter consistencia de FKs em ambiente local.
DROP TABLE IF EXISTS movimentacoes;
DROP TABLE IF EXISTS produtos;
DROP TABLE IF EXISTS categorias;
DROP TABLE IF EXISTS usuarios;

CREATE TABLE usuarios (
    id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
    nome VARCHAR(100) NOT NULL,
    login VARCHAR(50) NOT NULL,
    senha_hash VARCHAR(255) NOT NULL,
    ativo TINYINT(1) NOT NULL DEFAULT 1,
    criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    atualizado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT pk_usuarios PRIMARY KEY (id),
    CONSTRAINT uk_usuarios_login UNIQUE (login),
    CONSTRAINT chk_usuarios_nome_not_blank CHECK (CHAR_LENGTH(TRIM(nome)) > 0),
    CONSTRAINT chk_usuarios_login_not_blank CHECK (CHAR_LENGTH(TRIM(login)) > 0)
) ENGINE=InnoDB;

CREATE TABLE categorias (
    id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
    nome VARCHAR(100) NOT NULL,
    ativo TINYINT(1) NOT NULL DEFAULT 1,
    criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    atualizado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT pk_categorias PRIMARY KEY (id),
    CONSTRAINT uk_categorias_nome UNIQUE (nome),
    CONSTRAINT chk_categorias_nome_not_blank CHECK (CHAR_LENGTH(TRIM(nome)) > 0)
) ENGINE=InnoDB;

CREATE TABLE produtos (
    id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
    nome VARCHAR(150) NOT NULL,
    descricao TEXT NULL,
    categoria_id BIGINT UNSIGNED NOT NULL,
    preco_unitario DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    quantidade_atual INT UNSIGNED NOT NULL DEFAULT 0,
    estoque_minimo INT UNSIGNED NOT NULL DEFAULT 0,
    ativo TINYINT(1) NOT NULL DEFAULT 1,
    criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    atualizado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT pk_produtos PRIMARY KEY (id),
    CONSTRAINT fk_produtos_categoria FOREIGN KEY (categoria_id)
        REFERENCES categorias (id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,
    CONSTRAINT chk_produtos_nome_not_blank CHECK (CHAR_LENGTH(TRIM(nome)) > 0),
    CONSTRAINT chk_produtos_preco_non_negative CHECK (preco_unitario >= 0.00),
    CONSTRAINT chk_produtos_qtd_non_negative CHECK (quantidade_atual >= 0),
    CONSTRAINT chk_produtos_estoque_min_non_negative CHECK (estoque_minimo >= 0)
) ENGINE=InnoDB;

CREATE INDEX idx_produtos_categoria_id ON produtos (categoria_id);
CREATE INDEX idx_produtos_nome ON produtos (nome);
CREATE INDEX idx_produtos_ativo ON produtos (ativo);

CREATE TABLE movimentacoes (
    id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
    produto_id BIGINT UNSIGNED NOT NULL,
    tipo ENUM('ENTRADA', 'SAIDA') NOT NULL,
    quantidade INT UNSIGNED NOT NULL,
    motivo VARCHAR(255) NULL,
    usuario_id BIGINT UNSIGNED NULL,
    criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT pk_movimentacoes PRIMARY KEY (id),
    CONSTRAINT fk_movimentacoes_produto FOREIGN KEY (produto_id)
        REFERENCES produtos (id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,
    CONSTRAINT fk_movimentacoes_usuario FOREIGN KEY (usuario_id)
        REFERENCES usuarios (id)
        ON UPDATE CASCADE
        ON DELETE SET NULL,
    CONSTRAINT chk_movimentacoes_quantidade_positive CHECK (quantidade > 0)
) ENGINE=InnoDB;

CREATE INDEX idx_movimentacoes_produto_id ON movimentacoes (produto_id);
CREATE INDEX idx_movimentacoes_usuario_id ON movimentacoes (usuario_id);
CREATE INDEX idx_movimentacoes_criado_em ON movimentacoes (criado_em);
CREATE INDEX idx_movimentacoes_tipo ON movimentacoes (tipo);

-- Seed opcional para acesso inicial.
-- Login: admin
-- Senha em SHA256: admin123
INSERT INTO usuarios (nome, login, senha_hash, ativo)
SELECT 'Administrador', 'admin', SHA2('admin123', 256), 1
WHERE NOT EXISTS (
    SELECT 1
    FROM usuarios
    WHERE login = 'admin'
);
