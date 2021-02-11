using System.Threading.Tasks;

namespace App.LogicAppShared.Runtime.Scripts.CommonPatterns
{
    public interface IBuilder
    {
        IBuilder With<W>(W block) where W : class;
        
        Task<IResult> Build();
    }
    
    public interface IBuilder<T> : IBuilder
    {
    }
}

