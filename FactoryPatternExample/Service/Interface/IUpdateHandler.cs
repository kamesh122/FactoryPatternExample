namespace FactoryPatternExample.Service.Interface
{
    public interface IUpdateHandler<T> where T : class
    {
        Task<bool> UpdateAsync(T entity);
    }

}
