/*using System;


namespace Composer.Construction.Commands
{
    public class SequenceCommand : ICommand
    {
        public SampleTime ExecuteTime { get; private set; }
        public ICommand[] Commands { get; private set; }

        public SequenceCommand(SampleTime time, ICommand[] commands)
        {
            this.ExecuteTime = time;
            this.Commands = commands;
        }

        public void Execute(Synth synth)
        {
            //foreach ()
            
        }
    }

    /*public static class NoteExtensions
    {
        public static void Note(this IPerformable p, int note)
        {

        }
    }
}*/