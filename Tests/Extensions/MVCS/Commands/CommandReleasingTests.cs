using Build1.PostMVC.Extensions.MVCS.Commands;
using Build1.PostMVC.Extensions.MVCS.Commands.Impl;
using Build1.PostMVC.Extensions.MVCS.Injection.Impl;
using Build1.PostMVC.Tests.Extensions.MVCS.Commands.Parts;
using NUnit.Framework;

namespace Build1.PostMVC.Tests.Extensions.MVCS.Commands
{
    public sealed class CommandReleasingTests
    {
        private ICommandBinder _binder;
        
        [SetUp]
        public void SetUp()
        {
            _binder = new CommandBinder
            {
                InjectionBinder = new InjectionBinder()
            };
        }

        [Test]
        public void ReleasingTest()
        {
            Assert.That(() => _binder.ReleaseCommand(null), Throws.Exception);
            Assert.That(() => _binder.ReleaseCommand<int>(null), Throws.Exception);
            Assert.That(() => _binder.ReleaseCommand<int, string>(null), Throws.Exception);
            Assert.That(() => _binder.ReleaseCommand<int, string, CommandData>(null), Throws.Exception);
        }
    }
}