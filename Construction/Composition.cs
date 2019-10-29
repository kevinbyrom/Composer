using System;
using System.Collections.Generic;

namespace Composer.Construction
{
    public class Composition : IPerformable
    {
        public Synth Synth { get; protected set; }
        public int SampleRate { get; protected set; }

        private List<ICommand> commands;

        public Composition(Synth synth, int sampleRate)
        {
            this.Synth = synth;
            this.SampleRate = sampleRate;
            this.commands = new List<ICommand>();
        }

        public void AddCommand(ICommand cmd)
        {
            this.commands.Add(cmd);
        }

        public void Perform(ISampleTarget target)
        {            
            // While there are commands, execute them and render the synth voices to the target

            foreach (var cmd in commands)
            {
                cmd.Execute(this.Synth);

                // Update voices until done, rendering to target

            }
        }
    }
}