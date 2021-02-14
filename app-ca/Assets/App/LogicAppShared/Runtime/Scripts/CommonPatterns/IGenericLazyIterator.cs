using System;
using System.Collections;
using System.Collections.Generic;

namespace App.LogicAppShared.Runtime.Scripts.CommonPatterns
{
    public interface ILazyIterator<T> where T : Lazy<T>
    {
        GenericLazyIterator<T> GetIterator();
    }

    public static class ILazyIteratorExtensions
    {
        public static IEnumerator GetEnumerator<T>(this ILazyIterator<T> lazyIterator) where T : Lazy<T>
        {
            return lazyIterator.GetIterator().GetEnumerator();
        }
    }

    public abstract class GenericLazyIterator<T> : IEnumerable<T>, ILazyIterator<T>
        where T : Lazy<T>
    {
        protected Lazy<T> lazyLinkedValue;
        public virtual IEnumerator<T> GetEnumerator()
        {
            var currentIndex = lazyLinkedValue;
            while (currentIndex != null)
            {
                yield return currentIndex.Value;
                currentIndex = currentIndex.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public GenericLazyIterator<T> GetIterator()
        {
            return this;
        }
    }
}
