
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
}
