using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace QuizApplication.Models
{
    public class QuestionItemsController : Controller
    {
        private readonly QuizApplicationContext _context;

        public QuestionItemsController(QuizApplicationContext context)
        {
            _context = context;
        }

        // GET: QuestionItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.QuestionItems.ToListAsync());
        }

        // GET: QuestionItems/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionItem = await _context.QuestionItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionItem == null)
            {
                return NotFound();
            }

            return View(questionItem);
        }

        // GET: QuestionItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QuestionItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,QuestionText,IsComplete")] QuestionItem questionItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questionItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(questionItem);
        }

        // GET: QuestionItems/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionItem = await _context.QuestionItems.FindAsync(id);
            if (questionItem == null)
            {
                return NotFound();
            }
            return View(questionItem);
        }

        // POST: QuestionItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,QuestionText,IsComplete")] QuestionItem questionItem)
        {
            if (id != questionItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionItemExists(questionItem.Id))
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
            return View(questionItem);
        }

        // GET: QuestionItems/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionItem = await _context.QuestionItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionItem == null)
            {
                return NotFound();
            }

            return View(questionItem);
        }

        // POST: QuestionItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var questionItem = await _context.QuestionItems.FindAsync(id);
            _context.QuestionItems.Remove(questionItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionItemExists(long id)
        {
            return _context.QuestionItems.Any(e => e.Id == id);
        }
    }
}
