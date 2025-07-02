using Small_Library.ViewModels;
using ToDoApi.Data;
using ToDoApi.Interfaces;

namespace ToDoApi.Services;

public class AuditService : IAuditService
{
    private readonly ToDoDbContext _context;

    public AuditService(ToDoDbContext context)
    {
        _context = context;
    }

    public async Task LogActionAsync(string userId, string action, string details, string ipAddress)
    {
        var log = new AuditLog
        {
            UserId = userId,
            Action = action,
            Details = details,
            IPAddress = ipAddress,
        };

        _context.AuditLogs.Add(log);
        await _context.SaveChangesAsync();
    }
}