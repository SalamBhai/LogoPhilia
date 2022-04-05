using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheLogoPhilia.ApplicationDbContext;
using TheLogoPhilia.Entities;

namespace TheLogoPhilia.Implementations.Repositories
{
    public  class BaseRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        protected  AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> Create(T entity)
        {
             await _context.Set<T>().AddAsync(entity);
             await _context.SaveChangesAsync();
             return entity;
        }

        public bool AlreadyExists(Expression<Func<T, bool>> expression)
        {
            return  _context.Set<T>().Any(expression);
        }

        public async Task<T> Get(int id)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(L => L.Id == id && L.IsDeleted == false);
        }

        public async Task<ICollection<T>> Get()
        {
            return await _context.Set<T>().Where(a => a.IsDeleted == false).ToListAsync<T>();
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression)
        {
           return await _context.Set<T>().Where(expression).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<T> Update(T entity)
        {
            _context.Set<T>().Update(entity);
           await _context.SaveChangesAsync();
           return entity;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}