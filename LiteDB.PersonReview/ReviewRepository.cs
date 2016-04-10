using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectManager.Interfaces.PersonReview;
using ObjectManager.Model;
using ObjectManager.LiteDB.PersonReview.Model;
using LiteDB;

namespace ObjectManager.LiteDB.PersonReview
{
    class ReviewRepository : IReviewRepository
    {
        private readonly string _reviewConnection = DatabaseConnections.ReviewConnection;

        public List<Review> GetAll()
        {
            using (var db = new LiteDatabase(this._reviewConnection))
            {
                var repository = db.GetCollection<ReviewDB>("reviews");
                var results = repository.FindAll();

                return results.Select(x => Map(x)).ToList();
            }
        }

        public int Add(Review review)
        {
            using (var db = new LiteDatabase(this._reviewConnection))
            {
                var dbObject = InverseMap(review);

                var repository = db.GetCollection<ReviewDB>("reviews");
                if (repository.FindById(review.Id) != null)
                    repository.Update(dbObject);
                else
                    repository.Insert(dbObject);

                return dbObject.Id;
            }
        }

        public Review Get(int id)
        {
            using (var db = new LiteDatabase(this._reviewConnection))
            {
                var repository = db.GetCollection<ReviewDB>("reviews");
                var result = repository.FindById(id);
                return Map(result);
            }
        }

        public Review Update(Review review)
        {
            using (var db = new LiteDatabase(this._reviewConnection))
            {
                var dbObject = InverseMap(review);

                var repository = db.GetCollection<ReviewDB>("reviews");
                if (repository.Update(dbObject))
                    return Map(dbObject);
                else
                    return null;
            }
        }

        public bool Delete(int id)
        {
            using (var db = new LiteDatabase(this._reviewConnection))
            {
                var repository = db.GetCollection<ReviewDB>("reviews");
                return repository.Delete(id);
            }
        }

        internal List<ReviewDB> GetReviews(int[] ids)
        {
            using (var db = new LiteDatabase(this._reviewConnection))
            {
                var repository = db.GetCollection<ReviewDB>("reviews");
                var results = repository.FindAll().Where(x => ids.Contains(x.Id));

                return results.ToList();
            }
        }

        internal Review Map(ReviewDB dbReview)
        {
            if (dbReview == null)
                return null;

            var personRev = new PersonRepository();
            var author = personRev.Get(dbReview.AuthorID);
            return new Review() { Id = dbReview.Id, Content = dbReview.Content, Score = dbReview.Score, Author = author, MovieId = dbReview.MovieId };
        }

        internal ReviewDB InverseMap(Review review)
        {
            if (review == null)
                return null;
            return new ReviewDB() {
                Id = review.Id,
                Content = review.Content,
                Score = review.Score,
                AuthorID = review.Author.Id,
                MovieId = review.MovieId
            };
        }
    }


}
}
