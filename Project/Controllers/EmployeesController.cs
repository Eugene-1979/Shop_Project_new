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
    public class EmployeesController : Controller
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeesController(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
              return _employeeRepository._context.Employees != null ? 
                          View(await _employeeRepository.ModelAllAsync()) :
                          Problem("Entity set 'AppDbContent.Employees'  is null.");
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _employeeRepository._context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepository.ModelFirstofDefaultAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Salary,Email")] Employee employee)
        {
            if (ModelState.IsValid)
            {

                await _employeeRepository.ModelAddAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _employeeRepository._context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepository.ModelIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Salary,Email")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    await _employeeRepository.ModelUpdateAsync(employee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _employeeRepository._context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepository.ModelFirstofDefaultAsync(id); 
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_employeeRepository._context.Employees == null)
            {
                return Problem("Entity set 'AppDbContent.Employees'  is null.");
            }
            var employee = await _employeeRepository.ModelIdAsync(id);


            await _employeeRepository.ModelDeleteAsync(employee);
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id) => _employeeRepository.ModelExist(id);


    }
}
