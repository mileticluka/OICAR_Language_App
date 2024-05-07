using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IContextImageRepository
    {
        public ContextImage? GetContextImage(string path);
        public ContextImage? GetContextImage(int id);
        public int createContextImage(string path);
        IList<ContextImage> GetContextImages();
        void DeleteContextImage(ContextImage img);
    }
}
