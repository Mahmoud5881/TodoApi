using Microsoft.EntityFrameworkCore;
using ToDoApi;
using ToDoApi.Data;
using ToDoApi.Interfaces;

namespace ToDoApi.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ToDoDbContext context;

    public GenericRepository(ToDoDbContext context)
    {
        this.context = context;
    }
    

    public async Task Add(T entity)
    {
        await context.Set<T>().AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        context.Set<T>().Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task<List<T>> GetAll()
    {
        return await context.Set<T>().ToListAsync();
    }

    public async Task Update(T entity)
    {
        context.Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task<T> GetById(object id)
    {
        return await context.Set<T>().FindAsync(id);
    }
}