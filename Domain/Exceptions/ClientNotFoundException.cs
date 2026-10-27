namespace Domain.Exceptions;

public class ClientNotFoundException(long clientId) : Exception($"Client with id {clientId} was not found.")
{
    public long ClientId { get; } = clientId;
}