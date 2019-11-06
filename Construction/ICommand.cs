using System;


namespace Composer.Construction
{
    public interface ICommand 
    {
        SampleTime ExecuteTime { get; }
        void Execute(Synth synth);
    }
}