namespace Domain.Interface;

public interface IDetach<T>
{
    public void Detach(T entity);
}