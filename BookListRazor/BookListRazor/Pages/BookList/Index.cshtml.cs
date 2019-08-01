using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class IndexModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        //on the get we only need to pass the property
        //also does not need a post right now
        //Bind property is useful when you are actually posting something so that it is retrieved and you don't have to get that using parameters
        public IEnumerable<Book> Books { get; set; }


        //Value will be available only to the next request, after that it will lose its value
        [TempData]
        public string Message { get; set; }

        //Not an IActionResult because we are just assigning the books here
        public async Task OnGet()
        {
            //Retrieve all books and assign it to Books variable
            Books = await _db.Book.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var book = await _db.Book.FindAsync(id);
            if(book == null)
            {
                return NotFound();
            }
            _db.Book.Remove(book);
            await _db.SaveChangesAsync();
            Message = book.Name + " has been Deleted Successfully";
            return RedirectToPage("Index");
        }
    }
}