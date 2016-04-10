using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectManager.Model;

namespace ObjectManager.Interfaces.Movies
{
    public interface IMovieRepository
    {
        List<Movie> GetAll();
        int Add(Movie author);
        Movie Get(int id);
        Movie Update(Person author);
        bool Delete(int id);
    }
}
