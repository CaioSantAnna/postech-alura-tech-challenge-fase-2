Aluno - Caio Peres Sant Anna - rm352101

Contexto do projeto

	O projeto é a parte backend(APIs) de uma solução pensada para ser um "gerenciador de acervo". A ideia é possibilitar que alguém possa cadastrar itens diversos, objetos colecionáveis/que fazem parte de um acervo, como funkos, selos, quadrinhos, mangás,  moedas, jogos, livros, tênis, enfim qualquer tipo de item que alguém queira manter um cadastro de forma digitalizada.
	O projeto permite que cada usuário crie a sua própria estrutura para organização de itens através de categorias e subcategorias. A partir disso, é possível criar quaisquer atributos dos mais variados tipos para vincular a itens de acervo que serão cadastrados posteriormente.

Levantamento de Requisitos / Critérios de Aceite

	Possibilidade de incluir itens dos mais diversos tipos para gerenciamento de acervo/coleções
	Possibilidade de criar estruturas de organização para cadastro de itens com dois níveis
	Possibilidade de definir atributos para itens do acervo de forma livre
	Deve oferecer suporte a diversos tipos de atributos que armazenarão diversos tipos de informações, como números, inteiro e decimal, texto, imagem, data...
	Cada usuário deve ter acesso a gerenciar e visualizar apenas as suas próprias informações
	Possibilidade de cadastrar contatos vinculados ao cadastro de um usuário
	Possibilidade de marcar itens como "emprestado" vinculando ele a um determinado contato pré-existente
	Possibilidade de fazer pesquisa de itens pela estrutura criada (categoria + subcategoria) e nome de item cadastrado com recurso de paginação
	Possibilidade de pesquisar por nomes de itens da estrutra de organização criada (categoria) com recurso de paginação
	Possibilidade de fazer pesquisa por nome na lista de contatos cadastrado com recurso de paginação

Padrões, tecnologias e recursos utilizados no projeto

	.NET 7 - C# - JWT - Serilog - Sql Server - Dapper - Unit of Work + Repository - Injeção de dependência - Swagger + Gerenciamento de exceções  centralizado
		
Passos para build

	Essa aplicação foi desenvolvida com banco de dados SQL SERVER. Uma instância de SQL SERVER deve ser criada.
	Uma vez criada, há duas opções para seguir com a execução do projeto:

		Script executado manualmente - É possível executar o script "Criação Banco" para criar o banco e as tabelas utilizadas pela aplicação e o arquivo "Inserção Dados - Teste Inicial" para carregar dados na aplicação para que seja possível executar testes pelo swagger. Os scripts estão localizados em GerenciadorAcervo.Dados\Scripts.
		Restore de Banco - A segunda possibilidade é restaurar um banco pronto. Um backup com o nome "GerenciadorAcervo" contendo os mesmos dados que seriam inseridos manualmente pelo script "Inserção Dados - Teste Inicial" está disponível em GerenciadorAcervo.Dados\Backup Banco.

	Alterar a string de conexão no arquivo appsettings.json existente em GerenciadorAcervo.API

	Ao finalizar os passos acima, o projeto da API dentro da solução já pode ser "buildado"/executado. Caso a opção de executar scripts manualmente tenha sido utilizada, a tabela "Logs", utilizada no armazenamento de logs pelo Serilog, será criada de forma automática na primeira execução do projeto.

Na carga inicial de dados disponível para testes há 3 usuários previamente cadastrados:

	e-mail				nome			senha
	caiopsa@yahoo.com.br 		- Caio Sant Anna 	- *<!@#doppelganger>*
	alura@alura.com.br 		- Alura dos Santos 	- *<!@#doppelganger>*
	microsoft@microsoft.com.br 	- Microsoft Silva 	- *<!@#doppelganger>*

A pasta "Imagens Carga Inicial de Dados para Teste" na pasta raíz do projeto contém as imagens que foram utilizadas na carga inicial de dados da base do projeto. Nela há também arquivos com a versão base64 das imagens que podem ser utilizados para facilitar o cadastro de novos itens pelo swagger.