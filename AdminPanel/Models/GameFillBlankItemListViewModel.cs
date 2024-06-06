using DAL.Models;

namespace AdminPanel.Models
{
    public class GameFillBlankItemListViewModel
    {
        public IList<GameFillBlank> Games { get; set; }

        public int? LanguageId { get; set; }
        public string? Content { get; set; }
    }
}
