﻿namespace Domain.Interface;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(int id);
    Task<T> Add(T entity);
    Task<T> Update(T entity);
    Task<bool> Delete(int Id);
}