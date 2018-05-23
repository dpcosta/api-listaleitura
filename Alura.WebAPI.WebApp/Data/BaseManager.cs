using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.WebAPI.WebApp.Data
{
    /// <summary>
    /// Classe base responsável pelo código comum a um manager.
    /// </summary>
    /// <typeparam name="TEntity">Representa a entidade que será gerenciada</typeparam>
    /// <typeparam name="TKey">Representa o tipo da chave primária da entidade</typeparam>
    public class BaseManager<TEntity, TKey> : IManager<TEntity, TKey> where TEntity : class
    {

        private readonly ApplicationDbContext _context;
        public ApplicationDbContext Contexto => _context;

        public BaseManager(ApplicationDbContext contexto)
        {
            _context = contexto; //ainda existe um acoplamento aqui!
        }

        public IQueryable<TEntity> All => _context.Set<TEntity>().AsQueryable();

        public void Alterar(params TEntity[] obj)
        {
            _context.Set<TEntity>().UpdateRange(obj);
            _context.SaveChanges();
        }

        public void Excluir(params TEntity[] obj)
        {
            _context.Set<TEntity>().RemoveRange(obj);
            _context.SaveChanges();
        }

        public TEntity Find(TKey key)
        {
            return _context.Find<TEntity>(key);
        }

        public void Incluir(params TEntity[] obj)
        {
            _context.Set<TEntity>().AddRange(obj);
            _context.SaveChanges();
        }
    }
}
