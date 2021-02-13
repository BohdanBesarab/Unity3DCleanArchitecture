namespace App.LogicAppShared.Runtime.Scripts.CommonPatterns
{
    public interface IResult
    {

    }

    public interface IResult<T> : IResult where T : class
    {
        T GetResult();
    }
}