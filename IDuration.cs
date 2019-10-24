
namespace Composer
{
    public interface IDuration
    {
        void Update();
        bool IsDone { get; }
    }


    public class ManualDuration : IDuration 
    {
        public bool IsDone { get; private set;  }

        public void Set()
        {
            this.IsDone = true;
        }

        public void Update()
        {

        }
    }

    public class TimedDuration : IDuration 
    {
        int currFrames;
        int maxFrames;

        public bool IsDone { get; private set;  }

        public TimedDuration(int maxFrames)
        {
            this.currFrames = 0;
            this.maxFrames = maxFrames;
        }
        public void Set()
        {
            this.IsDone = true;
        }

        public void Update()
        {
            this.currFrames++;
            if (this.currFrames == this.maxFrames)                
            {
                IsDone = true;
            }
        }
    }
}
