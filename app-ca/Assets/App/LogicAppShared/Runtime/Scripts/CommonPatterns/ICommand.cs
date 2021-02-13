﻿namespace App.LogicAppShared.Runtime.Scripts.CommonPatterns
{
    public interface ICommand
    {
        void Execute();
    }

    public interface ICommand<T> : ICommand where T : class
    {
    }
}
