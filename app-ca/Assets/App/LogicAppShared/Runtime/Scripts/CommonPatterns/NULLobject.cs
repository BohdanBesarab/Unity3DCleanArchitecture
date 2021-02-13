namespace App.LogicAppShared.Runtime.Scripts.CommonPatterns
{
    public class NULLObject : IResult, IPrototype
    {
        public NULLObject()
        {
        }

        public virtual IPrototype Clone()
        {
            return new NULLObject();
        }
    }
}
