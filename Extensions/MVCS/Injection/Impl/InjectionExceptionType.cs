namespace Build1.PostMVC.Extensions.MVCS.Injection.Impl
{
    internal enum InjectionExceptionType
    {
        BindingAlreadyRegistered = 1,
        BindingIsMissing         = 2,
        BindingDoesntMatch       = 3,

        InstanceIsOfPrimitiveType = 5,
        InstanceIsMissing         = 6,

        ValueNotProvided  = 20,
        ValueNotDestroyed = 21,

        InjectionTypeMismatch = 40,

        CircularDependency                     = 100,
        CircularDependencyIsCounterMissing     = 101,
        CircularDependencyCounterIsAlreadyZero = 102
    }
}