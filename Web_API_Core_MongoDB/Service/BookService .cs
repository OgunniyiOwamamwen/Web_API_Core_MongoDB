using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Web_API_Core_MongoDB.Repository;
using MongoDB.Driver;
using Web_API_Core_MongoDB.Models;

namespace Web_API_Core_MongoDB.Service
{
    public class BookService
    {
        //Add a CRUD operations service

        private readonly IMongoCollection<Book> _books;
        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString); //My ConnectionString to Database
            var database = client.GetDatabase(settings.DatabaseName); //My Connection to DatabaseName

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        public List<Book> Get() => _books.Find(book => true).ToList();

        public Book Get(string id) => _books.Find<Book>(book => book.Id == id).FirstOrDefault();

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Book bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(Book bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) =>
            _books.DeleteOne(book => book.Id == id);

    }
}
