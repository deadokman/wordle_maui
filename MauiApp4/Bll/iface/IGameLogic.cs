using MauiApp4.Contracts;

namespace MauiApp4.Bll.iface
{
    public interface IGameLogic
    {
        int CurrentTryIdx { get; }
        int TryCount { get; }
        int WordLength { get; }

        void SetNewWord(string word);
        ValidateWorkResponse ValidateWord(string word);
    }
}