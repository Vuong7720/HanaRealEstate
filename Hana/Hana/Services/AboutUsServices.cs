using Hana.Data;
using Hana.Interfaces;
using Hana.Models.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace Hana.Services
{
    public class AboutUsServices : IAboutUsServices
    {

        private readonly HanaContext _context;
        public AboutUsServices(HanaContext context)
        {
            _context = context;
        }


        public IEnumerable<AboutUs> GetListAboutUs()
        {
            return _context.AboutUs.ToList();
        }

        public IQueryable<AboutUs> GetData()
        {
            return _context.AboutUs.AsQueryable();
        }
        public AboutUs GetDetails(int id)
        {
            return _context.AboutUs.Find(id);
        }

        public void Create(AboutUs about)
        {
            try
            {
                _context.AboutUs.Add(about);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        public void UpdateAboutUs(AboutUs about)
        {
            try
            {
                var a = _context.AboutUs.Find(about.Id);
                if (a != null)
                {
                    a.Content = about.Content;
                    _context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }



        public void DeleteAboutUs(int id)
        {
            try
            {
                var a = _context.AboutUs.Find(id);
                if (a != null)
                {
                    _context.AboutUs.Remove(a);
                    _context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        public bool IsExist(int id)
        {
            return _context.AboutUs.Any(a => a.Id == id);
        }


    }


}
