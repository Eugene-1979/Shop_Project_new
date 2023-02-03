using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop_Project.Db;
using Shop_Project.Models.Data;

namespace Shop_Project.Admin
{
    public class PagesDtoesController : Controller
    {
        private readonly AppDbContent _context;

        public PagesDtoesController(AppDbContent context)
        {
            _context = context;
        }

        // GET: PagesDtoes
        public async Task<IActionResult> Index()
        {
              return _context.PagesDtos != null ? 
                          View(await _context.PagesDtos.ToListAsync()) :
                          Problem("Entity set 'AppDbContent.PagesDtos'  is null.");
        }

        // GET: PagesDtoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PagesDtos == null)
            {
                return NotFound();
            }

            var pagesDto = await _context.PagesDtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pagesDto == null)
            {
                return NotFound();
            }

            return View(pagesDto);
        }

        // GET: PagesDtoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PagesDtoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Slug,Body,Sorting,HasSidebar")] PagesDto pagesDto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pagesDto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pagesDto);
        }

        // GET: PagesDtoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PagesDtos == null)
            {
                return NotFound();
            }

            var pagesDto = await _context.PagesDtos.FindAsync(id);
            if (pagesDto == null)
            {
                return NotFound();
            }
            return View(pagesDto);
        }

        // POST: PagesDtoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Slug,Body,Sorting,HasSidebar")] PagesDto pagesDto)
        {
            if (id != pagesDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pagesDto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagesDtoExists(pagesDto.Id))
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
            return View(pagesDto);
        }

        // GET: PagesDtoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PagesDtos == null)
            {
                return NotFound();
            }

            var pagesDto = await _context.PagesDtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pagesDto == null)
            {
                return NotFound();
            }

            return View(pagesDto);
        }

        // POST: PagesDtoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PagesDtos == null)
            {
                return Problem("Entity set 'AppDbContent.PagesDtos'  is null.");
            }
            var pagesDto = await _context.PagesDtos.FindAsync(id);
            if (pagesDto != null)
            {
                _context.PagesDtos.Remove(pagesDto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PagesDtoExists(int id)
        {
          return (_context.PagesDtos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
