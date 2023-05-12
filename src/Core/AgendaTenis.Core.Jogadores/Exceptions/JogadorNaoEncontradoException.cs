using System.Runtime.Serialization;

namespace AgendaTenis.Core.Jogadores.Exceptions;

public class JogadorNaoEncontradoException : Exception
{
    public JogadorNaoEncontradoException()
    {
    }

    public JogadorNaoEncontradoException(string? message) : base(message)
    {
    }

    public JogadorNaoEncontradoException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected JogadorNaoEncontradoException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
