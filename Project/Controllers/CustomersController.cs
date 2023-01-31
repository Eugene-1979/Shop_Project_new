﻿using System;
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
    public class CustomersController : Controller
    {

        private readonly CustomerRepository _customerRepository;

        public CustomersController(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
               
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
              return _customerRepository._context.Customers != null ? 
                          View(await _customerRepository.ModelAllAsync()) :
                          Problem("Entity set 'AppDbContent.Customers'  is null.");
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _customerRepository._context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _customerRepository.ModelFirstofDefaultAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email")] Customer customer)
        {
            if (ModelState.IsValid)
            {

                await _customerRepository.ModelAddAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _customerRepository._context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _customerRepository.ModelIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    await _customerRepository.ModelUpdateAsync(customer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _customerRepository._context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _customerRepository.ModelFirstofDefaultAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_customerRepository._context.Customers == null)
            {
                return Problem("Entity set 'AppDbContent.Customers'  is null.");
            }
            var customer = await _customerRepository.ModelIdAsync(id);


            await _customerRepository.ModelDeleteAsync(customer);
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id) => _customerRepository.ModelExist(id);


    }
}
