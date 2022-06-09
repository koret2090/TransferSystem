using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ComponentAccessToDB.RepositoryInterfaces
{
    public interface ICrudRepository<T>
    {
        bool Add(T element);
        List<T> GetAll();
        bool Update(T element);
        StatusCode Delete(int id);
    }
}
