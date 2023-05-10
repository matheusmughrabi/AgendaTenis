using AgendaTenis.BuildingBlocks.Notificacoes;
using AgendaTenis.BuildingBlocks.Notificacoes.Enums;
using FluentValidation;
using FluentValidation.Results;

namespace AgendaTenis.Core.Identity.Utils;

public static class NotificacoesExtensions
{
    public static List<Notificacao> ToNotificacao(this List<ValidationFailure> validacoesFluent)
    {
        return validacoesFluent.Select(validacao => new Notificacao()
        {
            Mensagem = validacao.ErrorMessage,
            Tipo = validacao.Severity.ToTipoNotificacao()
        }).ToList();
    }

    public static TipoNotificacaoEnum ToTipoNotificacao(this Severity severidadeFluent)
    {
        switch (severidadeFluent)
        {
            case Severity.Error:
                return TipoNotificacaoEnum.Erro;
            case Severity.Warning:
                return TipoNotificacaoEnum.Aviso;
            case Severity.Info:
                return TipoNotificacaoEnum.Informacao;
            default:
                throw new ArgumentException("severidade inválida");
        }
    }
}


