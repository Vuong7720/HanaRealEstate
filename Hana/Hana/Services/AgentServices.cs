using Hana.Data;
using Hana.Interfaces;
using Hana.Models.DataModels;
using Hana.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hana.Services
{
   

    public class AgentServices : IAgentServices
    {
        private readonly HanaContext _context;
        public AgentServices(HanaContext context)
        {
            _context = context;
        }

        

        public async Task<bool> Delete(int id)
        {
            var agent = await _context.Agent.FindAsync(id);
            if (agent != null)
            {
                _context.Remove(agent);
                await _context.SaveChangesAsync();
                return true;
            }
            else return false;
        }

        public async Task<VM_Agent> GetDetails(int id)
        {
            var agent = await _context.Agent.FindAsync(id);
            if (agent != null)
            {
                var posts = await _context.RealEstate
                                    .Include(r => r.RealEstateDetail)
                                    .Include(r => r.Map)
                                    .Where(r => r.AgentId == agent.Id)
                                    .ToListAsync();

                int total = posts.Count;
                int activePosts = posts.Where(p => p.IsActive).ToList().Count;
                var details = new VM_Agent()
                {
                    Id = agent.Id,
                    Name = agent.AgentName,
                    ContactNumber = agent.PhoneNumber,
                    TotalPosts = total,
                    ActivePosts = activePosts,
                    IsActive = agent.IsActive,
                    IsConfirmedNumber = agent.ConfirmPhoneNumber,
                    Posts = posts
                };
                return details;
            }
            return null;
        }

        public async Task<IEnumerable<Agent>> GetListAgent()
        {
            return await _context.Agent
                .Include(a => a.Level)
                .Where(a => a.LevelId > 1)
                .ToListAsync();
        }

        public VM_Agent GetUserInfo(int id)
        {
            var agent = _context.Agent.Find(id);

            if (agent == null) return null;

            return new VM_Agent()
            {
                Id = agent.Id,
                Name = agent.AgentName,
                ContactNumber = agent.PhoneNumber,
                Email = agent.Email ?? string.Empty
            };
        }
        public int UpdatePassword(VM_ChangePassword data)
        {
            try
            {
                var agent = _context.Agent.Find(data.Id);

                if (agent == null) return -1;

                if (agent.Password.Equals(data.OldPassword))
                {
                    agent.Password = data.NewPassword;
                    _context.SaveChanges();

                    return 1;
                }

                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public bool UpdateProfile(VM_Agent updateProfile)
        {
            try
            {
                var agent = _context.Agent.Find(updateProfile.Id);

                if (agent != null)
                {
                    agent.AgentName = updateProfile.Name;
                    agent.PhoneNumber = updateProfile.ContactNumber;
                    agent.Email = updateProfile.Email;
                    _context.SaveChanges();

                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Disable(int id)
        {
            var agent = await _context.Agent.FindAsync(id);
            if (agent != null)
            {
                if (!agent.IsActive) return false;
                agent.IsActive = false;
                await _context.SaveChangesAsync();
                return true;
            }
            else return false;
        }

        public async Task<bool> Enable(int id)
        {
            var agent = await _context.Agent.FindAsync(id);
            if (agent != null)
            {
                if (!agent.IsActive) 
                {
                    agent.IsActive = true;
                    await _context.SaveChangesAsync();
                    return true; 
                }
                return false;
            }
            return false;
        }

    }
}

