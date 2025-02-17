using FactoryPatternExample.Service.Interface;

namespace FactoryPatternExample.Service.Handlers
{
    public class UpdateHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public UpdateHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IUpdateHandler<T>? GetHandler<T>() where T : class
        {
            return _serviceProvider.GetService<IUpdateHandler<T>>();
        }
    }

}
