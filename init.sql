CREATE TABLE disco (
    id serial NOT NULL PRIMARY KEY,
    nome bigint NOT NULL,
    genero varchar(255) NOT NULL,
    preco real NOT NULL
);

CREATE TABLE venda (
    id serial NOT NULL PRIMARY KEY,
    data date NOT NULL
);

CREATE TABLE item_venda (
    id serial NOT NULL PRIMARY KEY,
    discoId bigint NOT NULL REFERENCES disco (id),
    vendaId bigint NOT NULL REFERENCES venda (id),
    valor real NOT NULL,
    cashback real NOT NULL
);

INSERT INTO disco (nome, genero, preco) values ("Thriller", "Pop", 10.0);
INSERT INTO disco (nome, genero, preco) values ("Beyoncé", "Pop", 10.0);
INSERT INTO disco (nome, genero, preco) values ("The Beatles", "Rock", 10.0);
INSERT INTO disco (nome, genero, preco) values ("Red Hot Chili Peppers", "Rock", 10.0);
INSERT INTO disco (nome, genero, preco) values ("Pink Floyd", "Rock", 10.0);
INSERT INTO disco (nome, genero, preco) values ("Metallica", "Rock", 10.0);