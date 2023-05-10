using AgendaTenis.BuildingBlocks.Notificacoes.Enums;

namespace AgendaTenis.BuildingBlocks.Notificacoes;

public class Notificacao
{
    public string Mensagem { get; set; }
    public TipoNotificacaoEnum Tipo { get; set; }
}
