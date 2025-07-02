using Microsoft.EntityFrameworkCore;

namespace ToDoApi.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task Update(T entity);
    Task Delete(T entity);
    Task<List<T>> GetAll();
    Task Add(T entity);
    Task<T> GetById(object id);
}