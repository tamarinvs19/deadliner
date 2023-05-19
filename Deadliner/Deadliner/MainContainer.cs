using Autofac;
using Autofac.Core;
using Deadliner.Api;
using Deadliner.Api.Controller;
using Deadliner.Api.Utils;
using Deadliner.Controller;
using Deadliner.Models;
using Deadliner.Utils;

namespace Deadliner;

public class MainContainer  // DI-container
{
    private static IContainer Container { get; set; }

    public static void BuildContainer()
    {
        var builder = new ContainerBuilder();
        // builder.RegisterType<IdGenerator>().As<IIdGenerator>().SingleInstance();
        builder.RegisterType<RandomIdGenerator>().As<IIdGenerator>().SingleInstance();
        builder.RegisterType<TimeProvider>().As<ITimeProvider>().InstancePerLifetimeScope();
        // builder.RegisterType<BaseContext>().As<IContext>().SingleInstance();
        // builder.RegisterType<AdoContext>().As<IContext>().SingleInstance();
        builder.RegisterType<MyDeadlinerContext>().As<IContext>().SingleInstance();
        builder.RegisterType<ActivityFactory>().As<IAbstractActivityFactory>();
        
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
    
    public static IAbstractActivityFactory AbstractActivityFactory()
    {
        using var scope = Container.BeginLifetimeScope();
        var activityFactory = scope.Resolve<IAbstractActivityFactory>();
        return activityFactory;
    }
    
    public static IContext Context()
    {
        using var scope = Container.BeginLifetimeScope();
        var context = scope.Resolve<IContext>();
        return context;
    }
}