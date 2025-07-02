namespace ToDoApi.Interfaces;

public interface IAuditService
{
    Task LogActionAsync(string userId, string action, string details, string ipAddress);
}