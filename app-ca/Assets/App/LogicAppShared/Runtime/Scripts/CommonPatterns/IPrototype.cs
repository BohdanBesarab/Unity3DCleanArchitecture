namespace App.LogicAppShared.Runtime.Scripts.CommonPatterns
{
    /// <summary>
    /// All classes uses this interface.
    /// a.k.a. ask everyone to fill in the data in a constructor like manner
    /// don't pass new object to all other to concrete classes to fill it's data
    /// </summary>
    public interface IPrototype
    {
        IPrototype Clone();
    }

    /// <summary>
    /// Fill the data for object. Make sure to copy it!
    /// </summary>
    public interface IPrototypeFill : IPrototype
    {
        void CloneTo(IPrototype fillMe);
    }
}
