namespace App.LogicAppShared.Runtime.Scripts.CommonPatterns
{
    public static class TrueNullCheck
    {
        public static bool IsNull(this UnityEngine.Object obj)
        {
            return obj == null;
        }
    }

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
