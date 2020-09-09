# API Books

## Desafio Tasklist
- [x] Criar uma API simples para buscar produtos no arquivo JSON disponibilizado.
- [x] Buscar livros por suas especificações(autor, nome do livro ou outro atributo)
- [x] É preciso que o resultado possa ser ordenado pelo preço.(asc e desc)
- [x] Disponibilizar um método que calcule o valor do frete em 20% o valor do livro.

## Passos para Execução
- Clonar o projeto
- Executar o projeto (dotnet run)

## URLs do Projeto
- Listar Todos os Livros
[GetAllBooks](https://localhost:5001/api/books)
- Listar Apenas um Livro
[GetById](https://localhost:5001/api/books/5)
- Calcular o frete de um Livro
[GetFrete](https://localhost:5001/api/books/3/frete)
- Ordernar pelo preço (order=all, order=desc, order=asc)
[Price](https://localhost:5001/api/books/price?order=desc)
- Buscar por nome (do Livro) ou Autor (?name=)(?author=)
[Search](https://localhost:5001/api/books/search?name=sea)
- Buscar por nome (do Livro) ou Autor (?name=)(?author=) e Ordenar por Preço (&order=)
[SearchAndOrderPrice](https://localhost:5001/api/books/search?name=the&order=desc)

## Detalhamento
- Foi criado um Service (BookService), o qual possui um Função Responsável pela leitura do JSON (ReadJson) e a Função Responsável por deserializar o JSON.

- ReadJson() -> utiliza o método httpClient para buscar o JSON disponibilizado ("https://raw.githubusercontent.com/timeiagro/BackendTest/master/books.json") e insere o conteúdo dele na variável apiResponse

- Deserialize() -> deserializa o objeto e insere em uma Lista de Livros e retorna essa lista.

- E por fim foram implementadas as seguintes funções:
  - GetAllBooks() -> responsável por buscar todos os livros;
  - GetById() -> responsável por retornar apenas o livro passado como parâmetro.
  - GetFrete() -> responsável por calcular o frete do livro passado como parâmetro.
  - OrderBy() -> responsável por realizar a ordenação de todos os livros a partir do preço.
  - Search() -> responsável por buscar pelo nome do livro ou nome do autor, podendo este resultado ser ordenado pelo preço.

- Controllers/BookController: a partir das rotas da aplicação realiza as chamadas utilizando 
