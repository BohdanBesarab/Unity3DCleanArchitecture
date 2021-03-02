using App.LogicAppShared.Runtime.Scripts.CommonPatterns;

namespace App.LogicAppShared.Tests
{
    public class TestSingletonFakeResource: SingletonSelfCreator<TestSingletonFakeResource>
        {
            protected override string PrefabPath {
                get
                {
                    return "SingletonResource";
                }
            }
        }
        public class TestSingletonPersistant : SingletonPersistent<TestSingletonPersistant>
        {
        }
        
        public class TestSingletonResourceEmpty: SingletonSelfCreator<TestSingletonResourceEmpty>
        {
            protected override string PrefabPath {
                get
                {
                    return null;
                }
            }
        } 
        
        public class TestSingletonResource: SingletonSelfCreator<TestSingletonResource>
        {
            protected override string PrefabPath {
                get
                {
                    return nameof(TestSingletonResource);
                }
            }
        }
        
        public class TestSingleton : Singleton<TestSingleton>
        {
        }
    
}