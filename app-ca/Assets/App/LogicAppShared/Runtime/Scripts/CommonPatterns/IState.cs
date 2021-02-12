using UnityEngine;

namespace App.LogicAppShared.Runtime.Scripts.CommonPatterns
{
    public interface IState
    {
        event System.Action<IState> OnEnter;
        event System.Action<IState> OnExit;
        
        void Enter();
        void Tick();
        void Exit();
    }
}
