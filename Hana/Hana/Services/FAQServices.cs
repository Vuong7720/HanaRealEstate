using Hana.Data;
using Hana.Interfaces;
using Hana.Models.DataModels;
using System.Collections.Generic;
using System.Linq;


namespace Hana.Services
{
   

    public class FAQServices : IFAQServices
    {
        private readonly HanaContext _context;
        public FAQServices(HanaContext context)
        {
            _context = context;
        }


        public IEnumerable<Faq> GetListFAQs()
        {
            return _context.Faq.ToList();
        }

        public Faq GetDetails(int id)
        {
            return _context.Faq.Find(id);
        }

        public void Create(Faq faq)
        {
            try
            {
                _context.Faq.Add(faq);
                _context.SaveChanges();
            }
            catch
            {

                throw;
            }

        }
        public void UpdateFAQ(Faq faq)
        {
            try
            {
                var a = _context.Faq.Find(faq.Id);
                if (a != null)
                {
                    a.Question = faq.Question;
                    a.Answer = faq.Answer;
                    _context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteFAQ(int id)
        {
            try
            {
                var a = _context.Faq.Find(id);
                if (a != null)
                {
                    _context.Faq.Remove(a);
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
            return _context.Faq.Any(f => f.Id == id);
        }


    }

}
