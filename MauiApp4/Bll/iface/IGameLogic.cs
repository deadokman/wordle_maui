using MauiApp4.Contracts;

namespace MauiApp4.Bll.iface
{
    public interface IGameLogic
    {
        int CurrentTryIdx { get; }

        int TryCount { get; }

        int WordLength { get; }

        void SetNewWord(string word, int tryCnt = 6);

        ValidateWorkResponse ValidateWord(string word);
    }
}