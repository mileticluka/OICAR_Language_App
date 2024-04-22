using DAL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGameRepository
    {
        public IList<T> GetGames<T>();
        public T GetRandomGame<T>();


        public Game GetGame(Type type, int id);
    }
}
