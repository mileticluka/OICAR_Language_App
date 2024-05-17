using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class Constants
    {
        public const string STAT_FILL_BLANK_PLAYED          = "fill_blank_played";
        public const string STAT_FILL_BLANK_COMPLETED       = "fill_blank_completed";
        public const string STAT_FLASH_CARDS_PLAYED         = "flash_cards_played";
        public const string STAT_FLASH_CARDS_COMPLETED      = "flash_cards_completed";
        public const string STAT_PICK_SENTENCE_PLAYED       = "pick_sentence_played";
        public const string STAT_PICK_SENTENCE_COMPLETED    = "pick_sentence_completed";

        public static IList<string> LIST_ALL_STATS = new List<string>() { 
            STAT_FILL_BLANK_COMPLETED, 
            STAT_FILL_BLANK_PLAYED,
            STAT_FLASH_CARDS_COMPLETED,
            STAT_FLASH_CARDS_PLAYED,
            STAT_PICK_SENTENCE_COMPLETED,
            STAT_PICK_SENTENCE_PLAYED
        };
    }
}
