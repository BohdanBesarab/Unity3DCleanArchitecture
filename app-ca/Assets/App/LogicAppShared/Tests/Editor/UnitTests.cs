using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using App.LogicAppShared.Runtime.Scripts.CommonPatterns;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace App.LogicAppShared.Tests
{
    public class UnitTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void UnitTestsSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A Test behaves as an ordinary method
        [Test]
        public void IBuilder()
        {
            IBuilder substituteBuilder = Substitute.For<IBuilder>();
            var objectToTest = substituteBuilder.With(new NULLObject());
            Assert.NotNull(objectToTest);
        }
        
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator UnitTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        private class SampleBuilder : IBuilder<NULLObject>
        {
            private System.Object data;

            public Task<IResult> Build()
            {
                if (data == null)
                {
                    return new Task<IResult>(() => new NULLObject());
                }

                return new Task<IResult>(() => data as NULLObject);
            }

            public IBuilder With<W>(W block) where W : class
            {
                data = block;
                return this;
            }
        }

        [Test]
        public async void IBuilderBuild()
        {
            IBuilder substituteBuilder = new SampleBuilder();
            var newNullObject = new NULLObject();
            var builder = substituteBuilder
                .With(newNullObject);
            Assert.NotNull(substituteBuilder);
            Assert.AreSame(substituteBuilder, builder);

            var result = await builder.Build();
            Assert.AreSame(result, newNullObject);
        }
        
        private T Setup<T>() where T : MonoBehaviour
        {
            var gameObject = new GameObject();
            return  gameObject.AddComponent<T>();
        }

        [UnityTest]
        public IEnumerator Singleton()
        {
            var singleton =  Setup<TestSingleton>();
            Assert.IsNotNull(TestSingleton.Instance);
            Assert.AreEqual(TestSingleton.Instance, singleton );
            GameObject.DestroyImmediate(singleton.gameObject);
            Assert.AreEqual(TestSingleton.IsExist, false);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator SingletonDoubles()
        {
            // Arrange Act Assert
            var singleton = TestSingleton.Instance;
            Assert.IsNotNull(TestSingleton.Instance);
            var singleton2 = Setup<TestSingleton>();
            int i = 5;
            while ( i>0)
            {
                i--;
                yield return null;
            }

            var gameObjectFromCopy = singleton2.gameObject;
            singleton2.CheckIsDuplicate();
            
            LogAssert.Expect(LogType.Warning, new Regex("[0-1000]*"));
            Assert.IsTrue( gameObjectFromCopy.IsNull());
            
            if (singleton != null)
            {
                GameObject.DestroyImmediate(singleton.gameObject);
            }

            if (singleton2 != null)
            {
                GameObject.DestroyImmediate(singleton2.gameObject);
            }
            Assert.AreEqual(TestSingleton.IsExist, false);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator SingletonPersistantDoubles()
        {
            // Arrange Act Assert
            var singleton = TestSingletonPersistant.Instance;
            Assert.IsNotNull(TestSingletonPersistant.Instance);
            var singleton2 = Setup<TestSingletonPersistant>();
            int i = 5;
            while ( i>0)
            {
                i--;
                yield return null;
            }

            var gameObjectFromCopy = singleton2.gameObject;
            singleton2.CheckIsDuplicate();
            
            LogAssert.Expect(LogType.Warning, new Regex("[0-1000]*"));
            Assert.IsTrue( gameObjectFromCopy.IsNull());
            
            if (singleton != null)
            {
                GameObject.DestroyImmediate(singleton.gameObject);
            }

            if (singleton2 != null)
            {
                GameObject.DestroyImmediate(singleton2.gameObject);
            }
            Assert.AreEqual(TestSingletonPersistant.IsExist, false);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator SingletonIsNotPersistant()
        {
            Assert.IsNotNull(TestSingleton.Instance);
            GameObject.DestroyImmediate(TestSingleton.Instance.gameObject);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator SingletonIsPersistant()
        {
            Assert.IsNotNull(TestSingletonPersistant.Instance);
            GameObject.DestroyImmediate(TestSingletonPersistant.Instance.gameObject);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator SingletonResourceFromEmpty()
        {
            Assert.IsNotNull(TestSingletonResourceEmpty.Instance);
            GameObject.DestroyImmediate(TestSingletonResourceEmpty.Instance.gameObject);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator SingletonResourceFromFake()
        {
            Assert.IsNotNull(TestSingletonFakeResource.Instance);
            LogAssert.Expect(LogType.Warning, new Regex("[0-1000]*"));
            GameObject.DestroyImmediate(TestSingletonFakeResource.Instance.gameObject);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator SingletonResourceFromReal()
        {
            var beforeLoad = Resources.Load(nameof(TestSingletonResource)) as GameObject;
            beforeLoad.AddComponent<TestSingletonResource>();
            Assert.IsNotNull(TestSingletonResource.Instance);
            LogAssert.NoUnexpectedReceived();
            GameObject.DestroyImmediate(TestSingletonResource.Instance.gameObject);
            yield return null;
        }


        internal class LazyValueIterator: LazyItem<int>
        {
            private LazyValueIterator previous;

            public LazyValueIterator(Func<int> function) : base(function) { }
            
            protected override LazyItem<int> GenerateNextValue()
            {
                var currentSum = Value;
                var newOne = new LazyValueIterator(()=>
                {
                    return currentSum + (this.previous == null? 1: this.previous.Value);
                });
                newOne.previous = this;
                return newOne;
            }

        }

        [Test]
        public void LazyList()
        {
            var newIterator = new LazyValueIterator(() =>
            {
                return 0;
            });
            
            int number = 5;
            int index = 0;
            int[] check = new []{0,1,1,2,3,5,8,13};
            foreach (var item in newIterator)
            {
                Assert.IsTrue(item == check[index]);
                index++;
                if (index > number)
                {
                    break;
                }
            }
            Assert.IsTrue(index > number);
        }

    }
}
