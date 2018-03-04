using System.Collections.Generic;
using System.Linq;
using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryServices
{
    public class LibraryAssetService : ILibraryAsset
    {
        private readonly LibraryContext _context;

        public LibraryAssetService(LibraryContext context)
        {
            _context = context;
        }

        public IEnumerable<LibraryAsset> GetAll()
        {
            return _context.LibraryAssets
                .Include(asset => asset.Status)
                .Include(asset => asset.Location);
        }

        public LibraryAsset GetById(int id)
        {
            return GetAll()
                .FirstOrDefault(asset => asset.Id == id);
        }

        public void Add(LibraryAsset newAsset)
        {
            _context.Add(newAsset);
            _context.SaveChanges();
        }

        public string GetAuthorOrDirector(int id)
        {
            var isBook = _context.LibraryAssets.OfType<Book>()
                .Any(asset => asset.Id == id);

            var isVideo = _context.LibraryAssets.OfType<Video>()
                .Any(asset => asset.Id == id);

            return isBook
                ? _context.Books.FirstOrDefault(book => book.Id == id)?.Author
                : _context.Videos.FirstOrDefault(video => video.Id == id)?.Director
                  ?? "Unknown";
        }

        public string GetDeweyIndex(int id)
        {
            // discriminator
            if (_context.Books.Any(book => book.Id == id))
                return _context.Books
                    .FirstOrDefault(book => book.Id == id)?.DeweyIndex;

            //var isBook = _context.LibraryAssets.OfType<Book>().Any(book => book.Id == id);

            return "";
        }

        public string GetType(int id)
        {
            var book = _context.LibraryAssets.OfType<Book>().Where(b => b.Id == id);
            return book.Any() ? "Book" : "Video";
        }

        public string GetTitle(int id)
        {
            return _context.LibraryAssets
                .FirstOrDefault(asset => asset.Id == id)
                ?.Title;
        }

        public string GetIsbn(int id)
        {
            if (_context.Books.Any(book => book.Id == id))
                return _context.Books
                    .FirstOrDefault(book => book.Id == id)?.ISBN;


            return "";
        }

        public LibraryBranch GetCurrentLocation(int id)
        {
            return GetById(id).Location;
        }
    }
}