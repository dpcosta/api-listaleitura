Evoluindo para uma API RESTful... (para que outras equipes/organizações utilizem precisamos comunicar a api através de um padrão)

## ListaLeitura (privada)
- GET 	/api/listaleitura - retorna as três listas de leitura para o usuário logado
- GET 	/api/listaleitura/{id} - retorna a lista de leitura, indicada por {id}, para o usuário logado

## Livros (privada)
- GET 	/api/livros - retorna os livros do usuário logado; códigos de retorno possíveis: 200, 204
- GET 	/api/livros/{id} - retorna o livro indicado por {id}; Códigos de retorno possíveis: 200, 404
- POST 	/api/livros - adiciona o livro na lista
- PUT		/api/livros - altera o livro 
- DELETE	/api/livros - remove o livro da lista

## Movimentação de livros (privada)
- PUT 	/api/movimentacao - move o livro informado de uma lista de origem para a lista de destino

## Pesquisa de livros (pública)
- GET 	/api/pesquisa/{termo} - retorna livros que atendem a pesquisa pelo {termo}