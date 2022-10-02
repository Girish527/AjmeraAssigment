using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjmeraAssignment.Models.Repository
{
    public interface IBookRepository<TEntity, TDto>
    {
        Task<IEnumerable<TEntity>> GetAll();
        TDto GetDto(string id);
        void Add(TEntity entity);
    }
}
