using System;


namespace Composer.Construction
{
    public interface ICommand 
    {
        void Execute(Synth synth);
    }
}