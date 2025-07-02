using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDoApi.Interfaces;
using ToDoApi.Models;
using ToDoApi.Repositories;

namespace ToDoApi.Services;

public class TodoService : ITodoService
{
    private readonly IGenericRepository<Todo> _repository;

    public TodoService(IGenericRepository<Todo> repository)
    {
        this._repository = repository;
    }
    
    public async Task<List<Todo>> GetAllAsync()
    {
        return await _repository.GetAll();
    }

    public async Task<Todo> GetByIdAsync(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task AddAsync(Todo todo)
    {
        await _repository.Add(todo);
    }

    public async Task UpdateAsync(Todo todo)
    {
        await _repository.Update(todo);
    }

    public async Task DeleteAsync(int id)
    {
        var todo = await _repository.GetById(id);
        if (todo != null)
        {
            await _repository.Delete(todo);
        }
    }
}