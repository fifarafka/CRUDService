using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectManager.Model;

namespace ObjectManager.Interfaces.PersonReview
{
    public interface IPersonRepository
    {
        List<Person> GetAll();
        int Add(Person author);
        Person Get(int id);
        Person Update(Person author);
        bool Delete(int id);
    }
}
