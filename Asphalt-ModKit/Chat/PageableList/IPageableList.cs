using Eco.Gameplay.Players;

namespace Asphalt.Chat.PageableList
{
    interface IPageableList
    {
        string HeaderMask { get; set; }
        string FooterMask { get; set; }
        int EntriesPerPage { get; set; }
        string[] Content { get; set; }

        int GetPageCount();
        bool IsValidPage(int page);
        void PrintHeader(User user, int page);
        void PrintFooter(User user, int page);
        void Print(User user, int page);
    }
}
