# Tic Tac Toe API

> An API with endpoints to a fake client reproducing a tic-tac-toe game platform

>Code Reviews very welcome! :)

## Notes

- The project was made using .NET Core 3.1.6 
- I used a static list in order to store objects in memory "simulating" a database access
- I will be trying to adapt the API using REST, SOLID and CLEAN architectures.
- Let me know if there is any other pattern that I can use
- I'm also trying to use the TDD method( Red -> Green -> Refactor)


## Install and Tests

```cmd
# Running main project
dotnet run --project "TicTacToeWebAPI"

# building main project
dotnet build 

# List acceptance tests for the API
dotnet test --list-tests

# Running API Tests
dotnet test

```
You can also test request through Swagger in localhost:5000 !


## Dependencies

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


## General Information

The goal of this project is to develop an API for a multiplayer game of Tic-Tac-Toe

## Details

The API must consist of all methods listed below.

## POST - /game

This endpoint must create a new game, returning the game id and the first player that wil be chosen randomly (X or O)

Example of the Data Transfer Object:

```
{
    "id" : "fbf7d720-df90-48c4-91f7-9462deafefb8",
    "firstPlayer": "X"
}
```
## POST - /game/{id}/movement

This call will register the player's movement.

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

The X and Y coordinates represent the position of the movement in the board. starting on the index 0 in the bottom left corner,
like so:

```
(x=0 y=2) | (x=1 y=2) | (x=2 y=2)
----------|-----------|----------
(x=0 y=1) | (x=1 y=1) | (x=2 y=1)
----------|-----------|----------
(x=0 y=0) | (x=1 y=0) | (x=2 y=0)
```
The player represents who is doing the movement. The input above for instance, would create the below movement:

```
          |           | 
----------|-----------|----------
    X     |           | 
----------|-----------|----------
          |           | 
```

In case of the movement was able to be played, the code 200 must be returned.

In case of the movement was not able to be played, an error must be returned.

ReturnValues:

**Wrong Turn**

```
{
    "msg": "Não é turno do jogador"
}
```

**Game Not Found**

```
{
    "msg": "Partida não encontrada"
}
```

Finnaly, if the game reaches its end, the return value must be:

```
{
    "msg": "Partida finalizada",
    "winner": "X"
}
```

If the game reaches a Draw, the 'player' attribute must be filled like so:

```
{
    "status": "Partida finalizada",
    "winner": "Draw"
}
```

