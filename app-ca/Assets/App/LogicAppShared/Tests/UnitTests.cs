using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.LogicAppShared.Runtime.Scripts.CommonPatterns;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
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
        public void IBuilderTest()
        { 
            IBuilder substituteBuilder = Substitute.For<IBuilder>();
            var objectToTest = substituteBuilder.With(new NULLObject());
            Assert.NotNull(objectToTest);
        }
        
        private class SampleBuilder: IBuilder<NULLObject>
        {
            private System.Object data;
                
            public Task<IResult> Build()
            {
                if (data == null)
                {
                    return new Task<IResult>(() =>  new NULLObject());
                }

                return new Task<IResult>( () =>  data as NULLObject );
            }

            public IBuilder With<W>(W block) where W : class
            {
                data = block;
                return this;
            }
        }
        
        [Test]
        public async void IBuilderBuildTest()
        {
            IBuilder substituteBuilder = new SampleBuilder();
            var newNullObject = new NULLObject();
            var builder =  substituteBuilder
                .With(newNullObject);
            Assert.NotNull(substituteBuilder);
            Assert.AreSame(substituteBuilder,builder );

            var result = await builder.Build();
            Assert.AreSame(result,newNullObject );
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
    }
}
