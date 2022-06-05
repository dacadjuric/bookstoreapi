using Application;
using Bogus;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreAPI.Core
{
    public class FakeData : IFakeData
    {
        private readonly BookstoreContext _context;

        public FakeData(BookstoreContext context)
        {
            _context = context;
        }

        public void AddAuthors()
        {
            var authorFaker = new Faker<Author>();

            authorFaker.RuleFor(x => x.FirstName, y => y.Name.FirstName());
            authorFaker.RuleFor(x => x.LastName, y => y.Name.LastName());
            authorFaker.RuleFor(x => x.Username, y => y.Internet.UserName());
            authorFaker.RuleFor(x => x.Password, y => y.Internet.Password());
            authorFaker.RuleFor(x => x.Email, y => y.Internet.Email());
            authorFaker.RuleFor(x => x.Telephone, y => y.Phone.PhoneNumber());

            var authors = authorFaker.Generate(10);
            _context.Authors.AddRange(authors);
            _context.SaveChanges();
        }

        public void AddBooks()
        {
            var bookFaker = new Faker<Book>();

            var publisherIds = _context.Publishers.Select(x => x.Id).ToList();

            bookFaker.RuleFor(x => x.Title, y => y.Lorem.Sentence());
            bookFaker.RuleFor(x => x.ISBN, y => y.Random.Long(1, 9999));
            bookFaker.RuleFor(x => x.PublishedDate, y => y.Date.Past(2022));
            bookFaker.RuleFor(x => x.TotalPages, y => y.Random.Number());
            bookFaker.RuleFor(x => x.PublisherId, y => y.PickRandom(publisherIds));

            var books = bookFaker.Generate(10);

            _context.Books.AddRange(books);
            _context.SaveChanges();

        }

        public void AddGenre()
        {
            throw new NotImplementedException();
        }

        public void AddGenres()
        {
            var genreFaker = new Faker<Genre>();

            genreFaker.RuleFor(x => x.GenreName, y => y.Hacker.Phrase());

            var genre = genreFaker.Generate(10);

            _context.Genres.AddRange(genre);
            _context.SaveChanges();
        }

        public void AddImage()
        {
            var bookIds = _context.Books.Select(x => x.Id).ToList();

            var imageFaker = new Faker<Image>();

            imageFaker.RuleFor(x => x.BookId, y => y.PickRandom(bookIds));
            imageFaker.RuleFor(x => x.Src, y => y.Image.PicsumUrl());
            imageFaker.RuleFor(x => x.Alt, y => y.Lorem.Sentence());

            var images = imageFaker.Generate(5);

            _context.Images.AddRange(images);
            _context.SaveChanges();
        }

        public void AddPublishers()
        {

            var publisherFaker = new Faker<Publisher>();

            publisherFaker.RuleFor(x => x.Address, y => y.Address.StreetAddress());
            publisherFaker.RuleFor(x => x.City, y => y.Address.City());
            publisherFaker.RuleFor(x => x.Name, y => y.Company.CompanyName());

            var publishers = publisherFaker.Generate(10);

            _context.Publishers.AddRange(publishers);
            _context.SaveChanges();
        }
    }

}
