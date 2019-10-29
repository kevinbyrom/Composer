using System;


namespace Composer.Construction.Commands
{
    public class NoteCommand : ICommand
    {
        public int Note;
        public double Duration;

        public NoteCommand(int note)
        {
        }

        public void Execute(Synth synth)
        {
            synth.PlayNote(this.Note, this.Duration);
        }
    }

    public static class NoteExtensions
    {
        public static void Note(this IPerformable p, int note)
        {

        }
    }
}