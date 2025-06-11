using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Entities;

namespace Dal.Api;

public interface Icurd<T>
{
    Task Create(T item);
    Task<List<T>> Read();
    Task Delete(T item);
    Task Update(T item);
}
