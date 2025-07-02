using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Small_Library.ViewModels;
using ToDoApi.Models;

namespace ToDoApi.Data
{
    public class ToDoDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
        
        public DbSet<AuditLog> AuditLogs { get; set; }
    }
} 