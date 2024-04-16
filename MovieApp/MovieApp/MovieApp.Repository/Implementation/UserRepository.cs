using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Models;
using MovieApp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<EShopApplicationUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<EShopApplicationUser>();
        }
        public IEnumerable<EShopApplicationUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public EShopApplicationUser Get(string id)
        {
            var strGuid = id.ToString();
            return entities
                .Include(z => z.order)
                .Include(z => z.order.ticketInOrder)
                .Include("order.ticketInOrder.ticket")
                .Include("order.ticketInOrder.ticket.Movie")
                .FirstOrDefault(s => s.Id == strGuid);
        }
        public void Insert(EShopApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(EShopApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(EShopApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }

}
