using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Models;
using MovieApp.Repository.Interface;

namespace MovieApp.Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {

        private readonly ApplicationDbContext context;
        private DbSet<T> entities;
        //string errorMessage = string.Empty;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public T Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
            return entity;
        }

        public T Get(Guid? id)
        {
            if (typeof(T).Equals(typeof(Ticket)))
            {
                return entities.Include("Movie").SingleOrDefault(s => s.id == id);
            }
            if (typeof(T).Equals(typeof(TicketInOrder)))
            {
                return entities.Include("ticket").Include("ticket.Movie").SingleOrDefault(s => s.id == id);
            }
            return entities.SingleOrDefault(s => s.id == id);
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T).Equals(typeof(Ticket)))
            {
                return entities.Include("Movie").AsEnumerable();
            }
            return entities.AsEnumerable();
        }

        public T Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public List<T> InsertMany(List<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            entities.AddRange(entities);
            context.SaveChanges();
            return entities;
        }

        public T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
            return entity;
        }
    }
}
