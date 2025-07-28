using Cysharp.Threading.Tasks;

namespace ContractsInterfaces
{
    public interface IView
    {
        UniTask Show();
        UniTask Hide();
    }
}
