namespace App.LogicAppShared.Runtime.Scripts.CommonPatterns
{
    public interface IChain
    {
        IChain NextHandler { get; }

        IChain SetNext(IChain nextHander);
    }


    public interface IChain<T> : IChain where T : class
    {
        void Handle(T request);
    }
}