using System;


namespace Composer.Construction
{
    public interface IPerformable 
    {
        void Perform(ISignalTarget target);
    }
}