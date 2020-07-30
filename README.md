# Tic Tac Toe API

>Uma API que provem endpoints para um cliente fictício contando com uma plataforma de Jogo da velha

>Code Reviews são muito bem-vindos! :)

## Observações

- O projeto foi feito em .NET Core 3.1.6 
- O build foi otimizado para ser um web deploy podendo ser hospedado via WebDeploy
- Foi utilizado uma lista estática para armazenar objetos em memória "simulando" um acesso a banco de dados
- Apesar de desnecessário devido a simplicidade da API, levei em consideração adotar alguns padrões de arquitetura SOLID e REST
- Padrões de projeto tornaram a API mais modular e dão maior longevidade ao código
- Foi utilizado da pratica de TDD para o desenvolvimento dos enpoints.


## Instalalação e Testes

```cmd
# Para rodar o projeto principal:
dotnet run --project "TicTacToeWebAPI"

# Para buildar o projeto principal:
dotnet build 

# Para visualizar os testes de aceitação da API:
dotnet test --list-tests

# Para rodar o modulo de testes de aceitação da API:
dotnet test

```

Você pode também testar as requests pelo Swagger na url localhost:5000 !



## Dependencias

```
O projeto 'TicTacToeWebAPI' tem as seguintes referências de pacote
   [netcoreapp3.1]:
   Pacote de Nível Superior         Solicitado   Resolvido
   > Microsoft.AspNetCore.Cors      2.2.0        2.2.0
   > Newtonsoft.Json                12.0.3       12.0.3
   > Swashbuckle.AspNetCore         5.5.1        5.5.1

O projeto 'TicTacToeWebAPI.Entities' tem as seguintes referências de pacote
   [netcoreapp3.1]: Nenhum pacote foi encontrado para essa estrutura.
O projeto 'TicTacToeWebAPI.Services' tem as seguintes referências de pacote
   [netcoreapp3.1]: Nenhum pacote foi encontrado para essa estrutura.
O projeto 'TicTacToeWebAPI.Boundaries' tem as seguintes referências de pacote
   [netcoreapp3.1]: Nenhum pacote foi encontrado para essa estrutura.
O projeto 'TicTacToeWebAPI.Mapper' tem as seguintes referências de pacote
   [netcoreapp3.1]: Nenhum pacote foi encontrado para essa estrutura.
O projeto 'TicTacToeWebAPI.Repositories' tem as seguintes referências de pacote
   [netcoreapp3.1]: Nenhum pacote foi encontrado para essa estrutura.
O projeto 'TicTacToeWebAPI.Utils' tem as seguintes referências de pacote
   [netcoreapp3.1]:
   Pacote de Nível Superior                      Solicitado   Resolvido
   > Microsoft.AspNetCore.Http.Abstractions      2.2.0        2.2.0
   > Unity.Interception.NetCore                  4.0.4        4.0.4
   > Unity.NetCore                               4.0.3        4.0.3

O projeto 'TicTacToeWebAPI.Tests' tem as seguintes referências de pacote
   [netcoreapp3.1]:
   Pacote de Nível Superior             Solicitado   Resolvido
   > coverlet.collector                 1.3.0        1.3.0
   > Microsoft.AspNetCore.TestHost      3.1.6        3.1.6
   > Microsoft.NET.Test.Sdk             16.6.1       16.6.1
   > MSTest.TestAdapter                 2.1.2        2.1.2
   > MSTest.TestFramework               2.1.2        2.1.2
```


## Enunciado Geral

O objetivo deste projeto é desenvolver uma __API__ para um jogo multiplayer de **Jogo da Velha**.

## Premissas

- Poderá ser feito em qualquer linguagem;
- Deverá conter um README com instruções claras de build e dependências;
- Build automatizado é opcional mas desejável;
- Não pode haver dependência de banco de dados ou serviços externos. A persistência dos dados pode
ser feita por exemplo in-memory ou baseada em arquivos;
- Não é necessária preocupação com autenticação dos métodos;
- __Será avaliado além do funcionamento da API boas práticas de desenvolvimento de software.__

## Detalhes

A api deverá conter os métodos abaixo. O funcionamento da partida será baseado em turnos, a cada
momento um jogador realiza a jogada.

## POST - /game

Essa chamada criara uma nova partida e retornará o id da partida criada. Além do id ele vai sortear qual
jogador ira começar a partida o "X" ou o "O".

Exemplo de retorno:

```
{
    "id" : "fbf7d720-df90-48c4-91f7-9462deafefb8",
    "firstPlayer": "X"
}
```
## POST - /game/{id}/movement

Essa chamada fará o movimento de cada jogador.

Input:

```
{
    "id" : "fbf7d720-df90-48c4-91f7-9462deafefb8",
    "player": "X",
    "position": {
        "x": 0 ,
        "y": 1
    }
}
```


As coordenadas X e Y representam a posição no tabuleiro do movimento. Começando do índice 0, no canto
inferior esquerdo. De forma que o tabuleiro fica assim:

```
(x=0 y=2) | (x=1 y=2) | (x=2 y=2)
----------|-----------|----------
(x=0 y=1) | (x=1 y=1) | (x=2 y=1)
----------|-----------|----------
(x=0 y=0) | (x=1 y=0) | (x=2 y=0)
```
O player representa o jogador que está fazendo a jogada. Esse input por exemplo criaria a jogada abaixo:

```
          |           | 
----------|-----------|----------
    X     |           | 
----------|-----------|----------
          |           | 
```

Caso a jogada seja feita com sucesso o código 200 deve ser retornado.

Caso não seja o turno do jogador, ou a partida não exista, um erro deve ser retornado.

Retorno:

**Turno Errado**

```
{
    "msg": "Não é turno do jogador"
}
```

**Partida Inexistente**

```
{
    "msg": "Partida não encontrada"
}
```
Finalmente se o jogo chegar ao fim o retorno deve ser assim:


```
{
    "msg": "Partida finalizada",
    "winner": "X"
}
```
Se o jogo deu **velha** (empate), o campo jogador ganhador deve vir preenchido da seguinte forma:


```
{
    "status": "Partida finalizada",
    "winner": "Draw"
}
```

