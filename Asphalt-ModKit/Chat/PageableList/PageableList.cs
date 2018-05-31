using System;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;

namespace Asphalt.Chat.PageableList
{
    public class PageableList : IPageableList
    {
        public string HeaderMask { get; set; }
        public string FooterMask { get; set; }
        public int EntriesPerPage { get; set; }
        public string[] Content { get; set; }

        public PageableList(string[] entries)
        {
            Content = entries;
            HeaderMask = "Header";
            FooterMask = "Footer";
            EntriesPerPage = 3;
        }

        public int GetPageCount()
        {
            return (Content.Length - 1) / (EntriesPerPage + 1);
        }

        public bool IsValidPage(int page)
        {
            return page > 0 && page < GetPageCount();
        }

        public void Print(User user, int page)
        {
            if (!IsValidPage(page))
                throw new ArgumentOutOfRangeException($"Page '{page}' is not a value between '0' and '{GetPageCount()}' in PageableList!");

            int startIndex = (EntriesPerPage * (page));
            int z = startIndex;
            while(z < Content.Length && z < startIndex + EntriesPerPage)
            {
                ChatManager.ServerMessageToPlayerAlreadyLocalized(Content[z], user);
                z++;
            }
        }

        public void PrintHeader(User user, int page)
        {
            ChatManager.ServerMessageToPlayerAlreadyLocalized(HeaderMask, user);
        }

        public void PrintFooter(User user, int page)
        {
            ChatManager.ServerMessageToPlayerAlreadyLocalized(FooterMask+$" page {page}/{GetPageCount()}", user);
        }
    }
}
