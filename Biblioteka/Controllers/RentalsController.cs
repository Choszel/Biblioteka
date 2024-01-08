using Biblioteka.Context;
using Biblioteka.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Controllers
{
    public class RentalsController : Controller
    {
        private readonly BibContext _context;

        public RentalsController(BibContext context)
        {
            _context = context;
        }

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            return _context.Rental != null ?
                        View(await _context.Rental.ToListAsync()) :
                        Problem("Entity set 'BibContext.Rental'  is null.");
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rental == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental
                .FirstOrDefaultAsync(m => m.rentalId == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rentals/Create
        public IActionResult Create()
        {
            ViewData["BookList"] = _context.Book.Select(c => new SelectListItem { Value = c.bookId.ToString(), Text = c.title }).ToList();
            return View();
        }

        // POST: Rentals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("rentalId,userId,rentalDate,rentalState,stateDate,rentalCity,rentalStreet")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                //rental.book = new List<Book>();
                //var bookList = (List<Book>)ViewData["bookList"];
                //foreach (var item in bookList)
                //{
                //    //_context.RentalBook.Add(new RentalBook(rental.rentalId, item.bookId));
                //    rental.book.Add(item);
                //}

                //if (ViewData.TryGetValue("bookList", out var bookListObject) && bookListObject != null)
                //{
                //    var bookList = (List<Book>)bookListObject;
                //    foreach (var item in bookList)
                //    {
                //        //_context.RentalBook.Add(new RentalBook(rental.rentalId, item.bookId));
                //        rental.book.Add(item);
                //    }
                //}
                //else
                //{
                //    ViewData["BookList"] = _context.Book.Select(c => new SelectListItem { Value = c.bookId.ToString(), Text = c.title }).ToList();
                //    return View(rental);
                //}

                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookList"] = _context.Book.Select(c => new SelectListItem { Value = c.bookId.ToString(), Text = c.title }).ToList();
            return View(rental);
        }

        public async void Create(Rental? rental, List<int> books)
        {
            if (ModelState.IsValid)
            {
                //rental.book = new List<Book>();
                //var bookList = (List<Book>)ViewData["bookList"];
                //foreach (var item in bookList)
                //{
                //    //_context.RentalBook.Add(new RentalBook(rental.rentalId, item.bookId));
                //    rental.book.Add(item);
                //}

                //if (ViewData.TryGetValue("bookList", out var bookListObject) && bookListObject != null)
                //{
                //    var bookList = (List<Book>)bookListObject;
                //    foreach (var item in bookList)
                //    {
                //        //_context.RentalBook.Add(new RentalBook(rental.rentalId, item.bookId));
                //        rental.book.Add(item);
                //    }
                //}
                //else
                //{
                //    ViewData["BookList"] = _context.Book.Select(c => new SelectListItem { Value = c.bookId.ToString(), Text = c.title }).ToList();
                //    return View(rental);
                //}

                _context.Add(rental);
                await _context.SaveChangesAsync();
            }
            return;
        }

        // GET: Rentals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rental == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            return View(rental);
        }

        // POST: Rentals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("rentalId,userId,rentalDate,rentalState,stateDate,rentalCity,rentalStreet")] Rental rental)
        {
            if (id != rental.rentalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalExists(rental.rentalId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(rental);
        }

        // GET: Rentals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rental == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental
                .FirstOrDefaultAsync(m => m.rentalId == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rental == null)
            {
                return Problem("Entity set 'BibContext.Rental'  is null.");
            }
            var rental = await _context.Rental.FindAsync(id);
            if (rental != null)
            {
                _context.Rental.Remove(rental);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalExists(int id)
        {
            return (_context.Rental?.Any(e => e.rentalId == id)).GetValueOrDefault();
        }
    }
}
