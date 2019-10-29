using System;


namespace Composer.Construction.Commands
{
    public class SequenceCommand : ICommand
    {
        public ICommand[] Commands { get; private set; }

        public SequenceCommand(int note)
        {
        }

        public void Execute(Synth synth)
        {
            foreach ()
            
        }
    }

    /*public static class NoteExtensions
    {
        public static void Note(this IPerformable p, int note)
        {

        }
    }*/
}