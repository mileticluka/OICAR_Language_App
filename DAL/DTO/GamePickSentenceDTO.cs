﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class GamePickSentenceDTO : GameDTO
    {
        public IList<string> Answers { get; set; }
        public string? AnswerSentence { get; set; }
    }
}
