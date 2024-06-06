using DAL.Models;

namespace AdminPanel.Models
{
    public class GameFlashCardItemListViewModel
    {
        public IList<GameFlashCard> Games { get; set; }

        public int? LanguageId { get; set; }
        public string? Content { get; set; }
    }
}
