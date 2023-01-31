using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop_Project.Db;
using Shop_Project.Models;
using Shop_Project.Repository;

namespace Shop_Project.Controllers
    {
    public class CategoriesController : Controller
        {
       
        private readonly CategoryRepository _categoryRepository;

        public CategoriesController(CategoryRepository categoryRepository)
            {
            
            _categoryRepository = categoryRepository;
            }

        // GET: Categories
        public async Task<IActionResult> Index()
            {
            return _categoryRepository._context.Categorys != null ?
                        View(await _categoryRepository.ModelAllAsync()) : /*repository*/
                        Problem("Entity set 'AppDbContent.Categorys'  is null.");
            }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
            {
            if(id == null || _categoryRepository._context.Categorys == null)
                {
                return NotFound();
                }

            var category = await _categoryRepository.ModelFirstofDefaultAsync(id); /*repository*/
            if(category == null)
                {
                return NotFound();
                }

            return View(category);
            }

        // GET: Categories/Create
        public IActionResult Create()
            {
            return View();
            }

        // POST: Categories/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Salary")] Category category)
            {
            if(ModelState.IsValid)
                {
               await _categoryRepository.ModelAddAsync(category);/*repository*/
                return RedirectToAction(nameof(Index));
                }
            return View(category);
            }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
            {
            if(id == null || _categoryRepository._context.Categorys == null)
                {
                return NotFound();
                }

            var category = await _categoryRepository.ModelIdAsync(id); /*repository*/
            if(category == null)
                {
                return NotFound();
                }
            return View(category);
            }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Salary")] Category category)
            {
            if(id != category.Id)
                {
                return NotFound();
                }

            if(ModelState.IsValid)
                {
                try
                    {

                    await _categoryRepository.ModelUpdateAsync(category); /*repository*/
                    }
                catch(DbUpdateConcurrencyException)
                    {
                    if(!CategoryExists(category.Id))
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
            return View(category);
            }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
            {
            if(id == null || _categoryRepository._context.Categorys == null)
                {
                return NotFound();
                }

            var category = await _categoryRepository.ModelFirstofDefaultAsync(id);
            if(category == null)
                {
                return NotFound();
                }

            return View(category);
            }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
            {
            if(_categoryRepository._context.Categorys == null)
                {
                return Problem("Entity set 'AppDbContent.Categorys'  is null.");
                }
            var category = await _categoryRepository.ModelIdAsync(id); /*repository*/


            await _categoryRepository.ModelDeleteAsync(category);
            return RedirectToAction(nameof(Index));
            }

        private bool CategoryExists(int id) => _categoryRepository.ModelExist(id); /*repository*/


        }
    }
