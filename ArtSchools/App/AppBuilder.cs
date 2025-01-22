using System.Collections.Concurrent;

namespace ArtSchools.App;

public class AppBuilder : IAppBuilder
{
    private readonly ConcurrentDictionary<string, bool> _registry = new();
    private readonly List<Action<IServiceProvider>> _buildActions;
    private readonly IServiceCollection _services;
    IServiceCollection IAppBuilder.Services => _services;
        
    public IConfiguration Configuration { get; }

    private AppBuilder(IServiceCollection services, IConfiguration configuration)
    {
        _buildActions = new List<Action<IServiceProvider>>();
        _services = services;
        Configuration = configuration;
    }

    public static IAppBuilder Create(IServiceCollection services, IConfiguration configuration = null)
        => new AppBuilder(services, configuration);

    public bool TryRegister(string name) => _registry.TryAdd(name, true);

    public void AddBuildAction(Action<IServiceProvider> execute)
        => _buildActions.Add(execute);

    public IServiceProvider Build()
    {
        IServiceProvider serviceProvider = _services.BuildServiceProvider();
        _buildActions.ForEach(a => a(serviceProvider));
        return serviceProvider;
    }
}