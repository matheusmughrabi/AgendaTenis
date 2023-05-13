using AgendaTenis.Core.Identity.AcessoDados;
using AgendaTenis.Core.Identity.Aplicacao.CriarConta;
using AgendaTenis.Core.Jogadores.AcessoDados;
using AgendaTenis.Core.Jogadores.Aplicacao.CompletarPerfil;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgendaTenis.WebApi.Bootstrapper.Tipos;

public class IdentityBootstrapper : IBootstrapper
{
    private readonly IdentityDbContext _identityDbContext;
    private readonly JogadoresDbContext jogadoresDbContext;
    private readonly IMediator _mediator;

    public IdentityBootstrapper(IdentityDbContext identityDbContext, JogadoresDbContext jogadoresDbContext, IMediator mediator)
    {
        _identityDbContext = identityDbContext;
        this.jogadoresDbContext = jogadoresDbContext;
        _mediator = mediator;
    }

    public async Task Inicializar()
    {
        Console.WriteLine("Início aplicar migrações");
        await AplicarMigracoes().ConfigureAwait(false);
        Console.WriteLine("Fim aplicar migrações");

        Console.WriteLine("Início criar contas");
        await CriarContas().ConfigureAwait(false);
        Console.WriteLine("Fim criar contas");
    }

    private async Task AplicarMigracoes()
    {
        await _identityDbContext.Database.MigrateAsync().ConfigureAwait(false);
        await jogadoresDbContext.Database.MigrateAsync().ConfigureAwait(false);
    }

    private async Task CriarContas()
    {
        var jaExistemRegistros = await _identityDbContext.Usuario.AnyAsync();

        if (jaExistemRegistros == false)
        {
            await CriarConta(new CriarUsuariosBootstrapModelo()
            {
                Email = "maria@gmail.com",
                Senha = "maria123456789",
                Nome = "Maria",
                Sobrenome = "Silva",
                DataNascimento = new DateTime(1980, 10, 15),
                Telefone = "5555-1111",
                Pais = "Brasil",
                Estado = "São Paulo",
                Cidade = "Campinas",
                MaoDominante = "Direita",
                Backhand = "Duas mãos",
                EstiloDeJogo = "Agressivo"
            }).ConfigureAwait(false);

            await CriarConta(new CriarUsuariosBootstrapModelo()
            {
                Email = "Claudio@gmail.com",
                Senha = "claudio123456789",
                Nome = "Claudio",
                Sobrenome = "Gomes",
                DataNascimento = new DateTime(1980, 10, 15),
                Telefone = "1234-5678",
                Pais = "Brasil",
                Estado = "São Paulo",
                Cidade = "Campinas",
                MaoDominante = "Direita",
                Backhand = "Duas mãos",
                EstiloDeJogo = "Defensivo"
            }).ConfigureAwait(false);

            await CriarConta(new CriarUsuariosBootstrapModelo()
            {
                Email = "joao@gmail.com",
                Senha = "joao123456789",
                Nome = "Joao",
                Sobrenome = "Pereira",
                DataNascimento = new DateTime(1980, 10, 15),
                Telefone = "3333-2222",
                Pais = "Brasil",
                Estado = "São Paulo",
                Cidade = "Campinas",
                MaoDominante = "Direita",
                Backhand = "Uma mão",
                EstiloDeJogo = "Balanceado"
            }).ConfigureAwait(false);

            await CriarConta(new CriarUsuariosBootstrapModelo()
            {
                Email = "Clara@gmail.com",
                Senha = "clara123456789",
                Nome = "Clara",
                Sobrenome = "Souza",
                DataNascimento = new DateTime(1980, 10, 15),
                Telefone = "6666-7777",
                Pais = "Brasil",
                Estado = "São Paulo",
                Cidade = "Campinas",
                MaoDominante = "Esquerda",
                Backhand = "Duas mãos",
                EstiloDeJogo = "Agressivo"
            }).ConfigureAwait(false);
        }
    }

    private async Task CriarConta(CriarUsuariosBootstrapModelo usuario)
    {
        var conta = await _mediator.Send(new CriarContaCommand() { Email = usuario.Email, Senha = usuario.Senha, SenhaConfirmacao = usuario.Senha }).ConfigureAwait(false);
        await _mediator.Send(new CompletarPerfilCommand()
        {
            UsuarioId = conta.Id.GetValueOrDefault(),
            Nome = usuario.Nome,
            Sobrenome = usuario.Sobrenome,
            DataNascimento = usuario.DataNascimento,
            Telefone = usuario.Telefone,
            Pais = usuario.Pais,
            Estado = usuario.Estado,
            Cidade = usuario.Cidade,
            MaoDominante = usuario.MaoDominante,
            Backhand = usuario.Backhand,
            EstiloDeJogo = usuario.EstiloDeJogo,
        }).ConfigureAwait(false);
    }
}

public class CriarUsuariosBootstrapModelo
{
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Telefone { get; set; }
    public string Pais { get; set; }
    public string Estado { get; set; }
    public string Cidade { get; set; }
    public string MaoDominante { get; set; }
    public string Backhand { get; set; }
    public string EstiloDeJogo { get; set; }
}

