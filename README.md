# AgendaTenis

## Índice

- [Sobre](#sobre)
- [Features](#features)
- [Valores de domínio](#valores_dominio)
- [Descrição técnica do sistema](#descricao_tecnica)
- [Como usar](#como_usar)
- [Considerações sobre o projeto](#consideracoes)


## Sobre<a name = "sobre"></a>

AgendaTenis é uma API na qual tenistas podem criar um perfil e agendar jogos com outros tênistas.\
A API conta com um sistema de pontuação que divide os tenistas em categorias (atp, avançado, intermediário e iniciante).\
O tênista pode buscar outros tênistas especificando a região (país, estado e cidade) e a categoria do adversário desejado e convidá-lo para um jogo.\
Quando um convite é feito, o adversário pode aceitar ou recusar o convite.\
Após o jogo, o desafiante registra o placar da partida e em seguida o adversário pode confirmar ou contestar o placar registrado.\
Se o adversário confirmar o placar, então a partida é validada e são adicionados 10 pontos para o vencedor e subtraídos 10 pontos do perdedor.

## Features<a name = "features"></a>

### Criar conta
Cadastro simples na plataforma com e-mail e senha.

**Rota**: Api/Identity/CriarConta\
**Método HTTP**: POST

### Gerar token (login)
Gera um token jwt para autenticação do usuário.\
Basta informar o e-mail e senha para obter o token.

**Rota**: Api/Identity/GerarToken\
**Método HTTP**: POST

### Completar perfil
Quando o usuário cria uma conta no sistema, ele fornece apenas as credenciais básicas (e-mail e senha).\
Para ter um perfil de tenista completo no sistema, o usuário deverá utilizar esta feature e informar alguns dados adicionais como:
- Nome
- Sobrenome 
- Data de nascimento
- Telefone
- País
- Estado
- Região
- Cidade
- Mão dominante
- Backhand
- Estilo de jogo

Com isso, o usuário terá um perfil completo que poderá ser encontrado por outros jogadores interessados em jogar com ele.

**Rota**: Api/Jogadores/Perfil/Completar\
**Método HTTP**: POST

### Buscar adversários
Essa feature é muito útil para o tênista encontrar adversários cadastrados na plataforma.\
É possível encontrarr outros tenistas filtrando por região e categoria.

**Rota**: Api/Jogadores/Adversarios/Buscar?pais=Brasil&estado=S%C3%A3o%20Paulo&cidade=Campinas&categoria=2\
**Método HTTP**: GET\
**Observações**: Se necessário utilize a seção [Valores de domínio](#valores_dominio) para encontrar os códigos para **Categoria**, **ModeloPartida**, **StatusConvite** e **StatusPlacar**

### Obter resumo
O usuário logado pode acessar este endpoint para obter seu resumo de tênista.\
Com isso ele irá obter as seguintes informações:
- Id
- Nome Completo
- Idade
- Pontuação
- Categoria

**Rota**: Api/Jogadores/Resumo\
**Método HTTP**: GET\
**Observações**: Se necessário utilize a seção [Valores de domínio](#valores_dominio) para encontrar os códigos para **Categoria**, **ModeloPartida**, **StatusConvite** e **StatusPlacar**

### Histórico de partidas
Esta feature busca as partidas do usuário logado (inclusive partidas canceladas e as que ainda não aconteceram).\
Para não onerar o banco de dados e a performance da aplicação, a consulta do histórico de partidas é paginada.\
Dessa forma o usuário precisa informar o número da página e os items por página.

**Rota**: Api/Partidas/Historico\
**Método HTTP**: GET\
**Observações**: Se necessário utilize a seção [Valores de domínio](#valores_dominio) para encontrar os códigos para **Categoria**, **ModeloPartida**, **StatusConvite** e **StatusPlacar**

### Convidar para jogar
Quando o usuário quiser convidar alguém para jogar, ele poderá utilizar esta feature.\
Para isso, basta informar as seguintes informações:
- AdversarioId (Este é o UsuarioId e não o JogadorId)
- DataDaPartida
- DescricaoLocal
- ModeloDaPartida

**Rota**: Api/Partidas/Convites/Convidar\
**Método HTTP**: POST

### Convites para jogar pendentes
Criei esta feature para que o usuário possa obter a lista de convites para jogar pendentes.\
Por exemplo, se o jogador A convidar o jogador B para uma partida, então quando o jogador B fizer login no sistema e acessar este endpoint,\ 
ele poderá ver o convite do jogador A.

**Rota**: Api/Partidas/Convites/Pendentes\
**Método HTTP**: GET

### Responder convite
Após verificar seus convites pendentes, o jogador poderá aceitar ou recusar os convites.\
Para isso, ele pode utilizar a feature Responder Convite na qual ele informa o Id da Partida (pode ser obtido utilizando a feature convites pendentes) e o status de aceitação (2 para aceitar e 3 para recusar).

**Rota*: Api/Partidas/Convites/Responder\
**Método HTTP**: POST\
**Observações**: Se necessário utilize a seção [Valores de domínio](#valores_dominio) para encontrar os códigos para **Categoria**, **ModeloPartida**, **StatusConvite** e **StatusPlacar**

### Registrar placar
Depois do jogo, o desafiante da partida poderá registrar o resultado na partida.

**Rota**: Api/Partidas/Placar/Registrar\
**Método HTTP**: POST

### Confirmar placar pendências
Com esta feature o usuário pode ver suas pendências de confirmação de placar.\
Observa-se que pendências de confirmação de placar existem quando:
1. Você é o adversário e jogou uma partida 
2. O desafiante da partida registrou o placar
3. Você ainda não confirmou o placar registrado pelo desafiante (dessa forma existe uma pendência de confirmação de placar)

**Rota**: Api/Partidas/Placar/Pendentes\
**Método HTTP**: GET

### Responder Placar
Essa feature conclui o ciclo de vida de uma partida.\
Ela deverá ser usada pelo adversário da partida para confirmar ou contestar o placar registrado pelo desafiante.\
Se o placar for confirmado, então as seguintes ações irão acontecer:

1. Vencedor é registrado na partida
2. Evento "Placar Confirmado" é emitido
3. Evento "Placar Confirmado" é consumido
    1. Vencedor ganha 10 pontos
    2. Perdedor perde 10 pontos

## Valores de domínio <a name = "valores_dominio">
Valores de numéricos domínio (enums) são utilizado em diversos locais da aplicação, tais como parâmetros de query (ie., faeture Buscar Jogadores), em requests http (ie., feature Responder Convite) e responses da api (ie., feature obter resumo do tenista).
Segue abaixo a lista de valores de domínio:
- Categoria
    - Atp = 1
    - Avançado = 2
    - Intermediário = 3
    - Iniciante = 4 

- Jogadores:
    - Desafiante = 1
    - Adversario = 2
    
- ModeloPartida:
    - SetUnico = 1
    - MelhorDeTresSets = 2
    - MelhorDeCincoSets = 3
    
- StatusConvite:
    - Pendente = 1
    - Aceito = 2
    - Recusado = 3
    
- StatusConvite:
    - AguardandoConfirmacao = 1
    - Aceito = 2
    - Contestado = 3

## Descrição técnica<a name = "descricao_tecnica"></a>
### Contextos delimitados
A aplicação possui 3 contextos delimitados que são totalmente desacoplados entre si.
Os 3 contextos são:
* Identity
* Jogadores
* Partidas

#### Contexto Identity
O contexto identity é utilizado apenas para realizar o cadastro de usuários na aplicação e geração de token de acesso jwt.

- Projeto: AgendaTenis.Core.Identity
- Modelo de dados:
    - Usuários
        - Id: string
        - Email: string 
        - Senha: string
- Banco de Dados: SQL Server
- Acesso a dados: O acesso a dados foi abstraído com uso do EntityFrameworkCore
- Observações:
    - Utilizei o "Repository Pattern" para não depender diretamente do EntityFrameworkCore
    - Utilizei o FluentValidation para realizar validações de dados
    - Utilizei o Mediatr para auxiliar na implementação do padrão Command
- Dependências:
    - AgendaTenis.BuildingBlocks.Notificacoes
    - FluentValidation
    - Mediatr
    - Microsoft.AspNetCore.Authentication.JwtBearer
    - Microsoft.EntityFrameworkCore.SqlServer
    - Microsoft.Extensions.Identity.Core

#### Contexto Jogadores
O contexto jogadores é utilizado para registrar o perfil do tênista e sua pontuação atual para que sua categoria possa ser definida

- Projeto: AgendaTenis.Core.Jogadores
- Modelo de dados:
    - Jogador
        - Id: Guid
        - UsuarioId: Guid
        - Nome: string
        - Sobrenome: string
        - DataNascimento: DateTime
        - Telefone: string
        - Pais: string
        - Estado: string
        - Cidade: string
        - MaoDominante: string
        - Backhand: string
        - EstiloDeJogo: string
        - PontuacaoId: Guid
- Enums:
    - CategoriaEnum:
        - Atp = 1
        - Avançado = 2
        - Intermediário = 3
        - Iniciante = 4  
- Banco de Dados: SQL Server
- Acesso a dados: O acesso a dados foi abstraído com uso do EntityFrameworkCore
- Observações:
    - Ao contrário do contexto identity, aqui eu **não** utilizei o "Repository Pattern". Dessa forma, estou injetando o DbContext diretamente nos fluxos. O motivo desta decisão foi testar um abordagem diferente do repository pattern.
    - **Não** Utilizei o FluentValidation para realizar validações de dados, criei validações simples utilizando POCO (Plain Old CLR Object)
    - Utilizei o Mediatr para auxiliar na implementação do padrão Command
- Dependências:
    - AgendaTenis.BuildingBlocks.Notificacoes
    - AgendaTenis.BuildingBlocks.Cache
    - AgendaTenis.BuildingBlocks.EventBus
    - Mediatr
    - Microsoft.EntityFrameworkCore.SqlServer

#### Contexto Partidas
O contexto de partidas registra todas as partidas já criadas no sistema.

- Projeto: AgendaTenis.Core.Partidas
- Modelo de dados:
    - Partida
        - Id: string
        - DesafianteId: string
        - AdversarioId: string
        - DataDaPartida: DateTime
        - DescricaoLocal: string
        - ModeloDaPartida: ModeloPartidaEnum
        - StatusConvite: StatusConviteEnum
        - StatusPlacar: StatusPlacarEnum
        - VencedorId: string
        - JogadorWO: string
            - Sets []: 
                - NumeroSet
                - GamesDesafiante
                - GamesAdversario
                - TiebreakDesafiante
                - TiebreakAdversario
- Enums:
    - Jogadores:
        - Desafiante = 1
        - Adversario = 2
    - ModeloPartida:
        - SetUnico = 1
        - MelhorDeTresSets = 2
        - MelhorDeCincoSets = 3
    - StatusConvite:
        - Pendente = 1
        - Aceito = 2
        - Recusado = 3
    - StatusConvite:
        - AguardandoConfirmacao = 1
        - Aceito = 2
        - Contestado = 3
- Banco de Dados: MongoDb
- Acesso a dados: O acesso a dados foi abstraído com uso do MongoDB.Driver
- Observações:
    - Utilizei o "Repository Pattern" para não depender diretamente do MongoDB.Driver
    - Utilizei o Mediatr para auxiliar na implementação do padrão Command
- Dependências:
    - AgendaTenis.BuildingBlocks.Notificacoes
    - AgendaTenis.BuildingBlocks.Cache
    - AgendaTenis.BuildingBlocks.EventBus
    - Mediatr
    - MongoDB.Driver

### Docker
- Criei um arquivo Dockerfile para o projeto AgendaTenis.WebApi
- Criei um arquivo docker-compose com todos os serviços necessário para a aplicação rodar
- Dessa forma a aplicação poderá ser executada apenas com as instruções presentes na seção [Como Usar](#como_usar)

## Como usar <a name = "como_usar"></a>
1. git clone {repourl}
2. cd .\AgendaTenis\
3. docker-compose pull
4. docker-compose build
5. docker-compose up

Observação: É um pré-requisito que você tenha o docker instalado em sua máquina

## Considerações sobre o projeto <a name = "consideracoes"></a>
1. Hoje só é possível convidar 1 jogador para a partida, ou seja, o sistema ainda não suporta partidas de duplas
2. Seria interessante criar uma interface de usuário para os tenistas utilizarem o sistema. Talvez um aplicativo mobile ou uma Web UI.
3. Ainda não criei testes de unidade. É algo que está no backlog.
4. No projeto AgendaTenis.Core.Identity, criei uma implementação bastante simples de cadastro de usuários. No futuro será interessante melhorar esta implementação, utilizando bibliotecas robustas como o Microsoft.AspNetCore.Identity que conta com um modelo de dados bastante completo para autenticação e autorização de usuário.
