﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class GameFlashCardDTO : GameDTO
    {
        public string Text { get; set; }

        public IList<string> Answers { get; set; }

        public string? Answer { get; set; }
    }
}
