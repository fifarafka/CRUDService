using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectManager.Model;

namespace ObjectManager.Interfaces.PersonReview
{
    public interface IReviewRepository
    {
        List<Review> GetAll();
        int Add(Review author);
        Review Get(int id);
        Review Update(Review author);
        bool Delete(int id);
    }
}
