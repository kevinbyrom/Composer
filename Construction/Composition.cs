using System;
using System.Collections.Generic;

namespace Composer.Construction
{
    public class Composition : IPerformable
    {
        public Synth Synth { get; protected set; }

        private List<ICommand> commands;

        public Composition(Synth synth)
        {
            this.Synth = synth;
            this.commands = new List<ICommand>();
        }

        public void AddCommand(ICommand cmd)
        {
            this.commands.Add(cmd);
        }
    }
}