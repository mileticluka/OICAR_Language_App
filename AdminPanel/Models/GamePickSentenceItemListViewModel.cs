using DAL.Models;

namespace AdminPanel.Models
{
    public class GamePickSentenceItemListViewModel
    {
        public IList<GamePickSentence> Games { get; set; }

        public int? LanguageId { get; set; }
        public string? Content { get; set; }
    }
}
