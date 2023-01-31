using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop_Project.Db;
using Shop_Project.Models;
using Shop_Project.Repository;

namespace Shop_Project.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductRepository _productRepository;
        public ILogger Log { get; }
        public ProductsController(ProductRepository productRepository,ILogger<ProductsController> log)
        {
            _productRepository=productRepository;
            Log = log;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _productRepository.ModelAllAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _productRepository._context.Products == null)
            {
                return NotFound();
            }

            var product = await _productRepository.ModelFirstofDefaultAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
    
            ViewData["CategoryId"] = new SelectList(_productRepository._context.Categorys, "Id", "Name");
            return View();
        }

        // POST: Products/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Sale,CategoryId,About,Reviews")] Product product)
        {

            /*  product.Category = _productRepository._context.Categorys.First(q => q.Id == product.CategoryId);*/
            (bool, string) value = _productRepository.CheckModel(product);

            if(value.Item1/*ModelState.IsValid*/)
                {
            
                await _productRepository.ModelAddAsync(product);
                return RedirectToAction(nameof(Index));
            }
            Log.LogInformation($"Create {DateTime.Now.ToString("d")} {this.GetType().Name} {value.Item2}");
          
            ViewData["Error"] = value.Item2;
            ViewData["CategoryId"] = new SelectList(_productRepository._context.Categorys, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
       [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _productRepository._context.Products == null)
            {
                return NotFound();
            }

            var product = await _productRepository.ModelIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_productRepository._context.Categorys, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CategoryId,Category,Sale,About,Reviews")] Product product)
        {


        /*My Validasion*/
            (bool, string) value = _productRepository.CheckModel(product);

            if (id != product.Id)
            {
                return NotFound();
            }
            /*  product.Category=_productRepository._context.Categorys.FirstOrDefault(x => x.Id == product.CategoryId);*/

            ModelState.Remove(nameof(Category));

            if (ModelState.IsValid)
            {
                try
                {

                    await _productRepository.ModelUpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            Log.LogInformation($"Create {DateTime.Now.ToString("d")} {this.GetType().Name} {value.Item2}");
            ViewData["Error"] = value.Item2;


            ViewData["CategoryId"] = new SelectList(_productRepository._context.Categorys, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _productRepository._context.Products == null)
            {
                return NotFound();
            }

            var product = await _productRepository.ModelFirstofDefaultAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_productRepository._context.Products == null)
            {
                return Problem("Entity set 'AppDbContent.Products'  is null.");
            }
            var product = await _productRepository.ModelIdAsync(id);


            await _productRepository.ModelDeleteAsync(product);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id) => _productRepository.ModelExist(id);




        /*Валидациия string*//*
        public JsonResult CheckEmptyString(string message) => Json(true);*/

    }
}
