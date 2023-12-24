/*using System;


namespace Composer.Construction.Commands
{
    public class NoteCommand : ICommand
    {
        public SampleTime ExecuteTime { get; private set; }
        public int Note;
        public double Duration;

        public NoteCommand(SampleTime time, int note, double duration)
        {
            this.ExecuteTime = time;
            this.Note = note;
            this.Duration = duration;
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
}*/