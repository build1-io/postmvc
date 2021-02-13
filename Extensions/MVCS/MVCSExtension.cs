using Build1.PostMVC.Contexts;
using Build1.PostMVC.Extensions.MVCS.Commands;
using Build1.PostMVC.Extensions.MVCS.Commands.Impl;
using Build1.PostMVC.Extensions.MVCS.Context;
using Build1.PostMVC.Extensions.MVCS.Events;
using Build1.PostMVC.Extensions.MVCS.Events.Impl;
using Build1.PostMVC.Extensions.MVCS.Injection;
using Build1.PostMVC.Extensions.MVCS.Injection.Impl;
using Build1.PostMVC.Extensions.MVCS.Mediation;
using Build1.PostMVC.Extensions.MVCS.Mediation.Impl;
using Build1.PostMVC.Modules;

namespace Build1.PostMVC.Extensions.MVCS
{
    public sealed class MVCSExtension : Extension
    {
        public IEventDispatcher EventDispatcher { get; }
        public IInjectionBinder InjectionBinder { get; }
        public ICommandBinder   CommandBinder   { get; }
        public IMediationBinder MediationBinder { get; }

        public MVCSExtension() : this(MediationMode.Strict)
        {
        }

        public MVCSExtension(MediationMode mediationMode)
        {
            EventDispatcher = new EventDispatcherWithCommandProcessing();
            InjectionBinder = new InjectionBinder();
            CommandBinder = new CommandBinder();
            MediationBinder = new MediationBinder(mediationMode);
        }

        public override void OnInitialized()
        {
            InjectionBinder.Bind<IContext>().ToValue(Context);
            InjectionBinder.Bind<IEventDispatcher>().ToValue(EventDispatcher).ConstructOnStart();
            InjectionBinder.Bind<IInjectionBinder>().ToValue(InjectionBinder);
            InjectionBinder.Bind<ICommandBinder>().ToValue(CommandBinder).ConstructOnStart();
            InjectionBinder.Bind<IMediationBinder>().ToValue(MediationBinder).ConstructOnStart();
        }

        public override void OnDispose()
        {
            InjectionBinder.UnbindAll();
        }

        public override void OnModuleConstructed(IModule module)
        {
            InjectionBinder.Construct(module, true);
        }

        public override void OnModuleDispose(IModule module)
        {
            InjectionBinder.Destroy(module, true);
        }

        public override void OnContextStarting()
        {
            InjectionBinder.ForEachBinding(binding =>
            {
                if (binding.ToConstructOnStart)
                    InjectionBinder.GetInstance(binding);
            });
        }

        public override void OnContextStarted()
        {
            InjectionBinder.GetInstance<IEventDispatcher>().Dispatch(ContextEvent.Started);
        }

        public override void OnContextStopped()
        {
            InjectionBinder.GetInstance<IEventDispatcher>().Dispatch(ContextEvent.Stopped);
        }
    }
}