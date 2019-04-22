# VinilApp

# Configurações do Postgres
 - Instale o PostgreSQL: https://www.postgresql.org/;
 - Coloque as configurações de 'user name' root, 'password' vazia, 'host' localhost e 'port' 5432;
 - Crie um banco de dados com CREATE DATABASE database_name;
 - Execute o script init.sql que está na raiz do projeto.
 
# Configurações da aplicação
 - Instale o Docker e Docker Compose: https://www.docker.com/;
 - Execute o comando em um terminal ou pronpt de comando na raiz do projeto: docker-compose up
 - Acesse a URL: http://localhost:5000/api/
 
# URIs
 - /disco/{qualquer_valor_inteiro}
 - /disco?paginaAtual={qualquer_valor_inteiro}&tamanhoPagina={qualquer_valor_inteiro}
 - /venda/{qualquer_valor_inteiro}
 - /venda?paginaAtual={qualquer_valor_inteiro}&tamanhoPagina={qualquer_valor_inteiro}
