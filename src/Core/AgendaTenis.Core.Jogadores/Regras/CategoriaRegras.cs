using AgendaTenis.Core.Jogadores.Enums;

namespace AgendaTenis.Core.Jogadores.Regras;

public class CategoriaRegras
{
    public double ObterPontuacaoMinima(CategoriaEnum categoria)
    {
        switch (categoria)
        {
            case CategoriaEnum.Atp:
            {
                return 1000;
            }
            case CategoriaEnum.Avancado:
            {
                return 750;
            }
            case CategoriaEnum.Intermediario:
            {
                return 500;
            }
            case CategoriaEnum.Iniciante:
            {
                return 0;
            }
            default:
            {
                return 0;
            };
        }
    }

    public double ObterPontuacaoMaxima(CategoriaEnum categoria)
    {
        switch (categoria)
        {
            case CategoriaEnum.Atp:
                {
                    return double.MaxValue;
                }
            case CategoriaEnum.Avancado:
                {
                    return 1000;
                }
            case CategoriaEnum.Intermediario:
                {
                    return 750;
                }
            case CategoriaEnum.Iniciante:
                {
                    return 500;
                }
            default:
            {
                return double.MaxValue;
            };
        }
    }
}
