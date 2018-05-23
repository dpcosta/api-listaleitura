using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.WebAPI.WebApp.Data
{
    interface IManager<TEntity, TKey> where TEntity : class
    {
        IQueryable<TEntity> All { get; }
        TEntity Find(TKey key);
        void Incluir(params TEntity[] obj);
        void Alterar(params TEntity[] obj);
        void Excluir(params TEntity[] obj);
    }
}
