

namespace Test
{
    public interface IGameManager
    {
        ManagerStatus status { get; }

        void StartUp();
    }
}
