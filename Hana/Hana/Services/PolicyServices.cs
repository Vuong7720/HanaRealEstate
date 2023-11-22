using Hana.Data;
using Hana.Interfaces;
using Hana.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hana.Services
{
    
    public class PolicyServices : IPolicyServices
    {
        private readonly HanaContext _context;
        public PolicyServices(HanaContext context)
        {
            _context = context;
        }
        public IEnumerable<Policy> GetListPolicies()
        {
            return _context.Policy.ToList();
        }

        public Policy GetDetails(int id)
        {
            return _context.Policy.Find(id);
        }

        public void Create(Policy policy)
        {
            try
            {
                _context.Policy.Add(policy);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdatePolicy(Policy policy)
        {
            try
            {
                var pl = _context.Policy.Find(policy.Id);
                if (pl != null)
                {
                    pl.PolicyContent = policy.PolicyContent;
                    _context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        public void DeletePolicy(int id)
        {
            try
            {
                var a = _context.Policy.Find(id);
                if (a != null)
                {
                    _context.Policy.Remove(a);
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
            return _context.Policy.Any(p => p.Id == id);
        }


    }
}
