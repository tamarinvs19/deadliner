using Autofac;
using Autofac.Core;
using Deadliner.Controller;
using Deadliner.Models;
using Deadliner.Utils;

namespace Deadliner;

public class MainContainer
{
    private static IContainer Container { get; set; }
    
    public static void BuildContainer()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<IdGenerator>().As<IIdGenerator>().SingleInstance();
        builder.RegisterType<TimeProvider>().As<ITimeProvider>().InstancePerLifetimeScope();
        builder.RegisterType<BaseContext>().As<IContext>();
        
        // builder.RegisterType<ActivityFactory>()
        //     .As<IAbstractActivityFactory>()
        //     .WithParameter("context", Context());
        
        Container = builder.Build();
    }

    public static ITimeProvider TimeProvider()
    {
        using var scope = Container.BeginLifetimeScope();
        var timeProvider = scope.Resolve<ITimeProvider>();
        return timeProvider;
    }
    
    public static IIdGenerator IdGenerator()
    {
        using var scope = Container.BeginLifetimeScope();
        var idGenerator = scope.Resolve<IIdGenerator>();
        return idGenerator;
    }
    
    // public static IAbstractActivityFactory AbstractActivityFactory()
    // {
    //     using var scope = Container.BeginLifetimeScope();
    //     var activityFactory = scope.Resolve<IAbstractActivityFactory>();
    //     return activityFactory;
    // }
    
    public static IContext Context()
    {
        using var scope = Container.BeginLifetimeScope();
        var context = scope.Resolve<IContext>();
        return context;
    }
}