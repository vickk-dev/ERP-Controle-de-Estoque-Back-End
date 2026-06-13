namespace Ferramenteiro.Domain.Entities;

public class DocumentoDuplicadoException : Exception
{
    public DocumentoDuplicadoException(string message) : base(message) { }
}