using DAL.Interfaces;
using DAL.Models;
using System.IO;

namespace DAL.Repository
{
    public class ContextImageRepository : IContextImageRepository
    {

        private readonly DataContext ctx;

        public ContextImageRepository(DataContext context)
        {
            ctx = context;
        }

        public int createContextImage(string path)
        {
            ContextImage img = new ContextImage() { ImagePath = path };
            ctx.ContextImage.Add(img);
            return img.Id;
        }

        public ContextImage? GetContextImage(string path)
        {
            return ctx.ContextImage.FirstOrDefault(img => img.ImagePath == path);
        }

        public ContextImage? GetContextImage(int id)
        {
            return ctx.ContextImage.FirstOrDefault(img => img.Id == id);
        }

        public void DeleteContextImage(ContextImage img)
        {
            ctx.ContextImage.Remove(img);
            ctx.SaveChanges();
        }

        public IList<ContextImage> GetContextImages()
        {
            return ctx.ContextImage.ToList();
        }
    }
}
