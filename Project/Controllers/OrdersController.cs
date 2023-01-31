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
    public class OrdersController : Controller
    {
        private readonly OrderRepository _orderRepository;

        public OrdersController(OrderRepository orderRepository)
            {
            _orderRepository = orderRepository;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            
            return View(await _orderRepository.ModelAllAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _orderRepository._context.Orders == null)
            {
                return NotFound();
            }

            var order = await _orderRepository.ModelFirstofDefaultAsync(id);
            if (order == null)
            {
                return NotFound();
            }
          ViewBag.Enrollment =_orderRepository._context.Enrollment.Where(q=>q.OrderId== id).ToList();



                
            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_orderRepository._context.Customers, "Id", "Name");
            ViewData["EmployeeId"] = new SelectList(_orderRepository._context.Employees, "Id", "Email");
            ViewBag.Products = _orderRepository._context.Products.ToList();
            ViewBag.q = new List<int>() { 1, 2 };
            return View();
        }

        // POST: Orders/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,MyDate,CustomerId")] Order order)
        {

            ModelState.Remove(nameof(Customer));
            ModelState.Remove(nameof(Employee));

            if (ModelState.IsValid)
            {

                await _orderRepository.ModelAddAsync(order);
                return RedirectToAction(nameof(Index));
            }
/*            ViewData["CustomerId"] = new SelectList(_orderRepository._context.Customers, "Id", "Name", order.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_orderRepository._context.Employees, "Id", "Email", order.EmployeeId);*/
            return View(/*order*/);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _orderRepository._context.Orders == null)
            {
                return NotFound();
            }

            var order = await _orderRepository.ModelIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_orderRepository._context.Customers, "Id", "Name", order.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_orderRepository._context.Employees, "Id", "Email", order.EmployeeId);
            return View(order);
        }

        // POST: Orders/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,MyDate,CustomerId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   
                    await _orderRepository.ModelUpdateAsync(order);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["CustomerId"] = new SelectList(_orderRepository._context.Customers, "Id", "Name", order.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_orderRepository._context.Employees, "Id", "Email", order.EmployeeId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _orderRepository._context.Orders == null)
            {
                return NotFound();
            }

            var order = await _orderRepository.ModelFirstofDefaultAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_orderRepository._context.Orders == null)
            {
                return Problem("Entity set 'AppDbContent.Orders'  is null.");
            }
            var order = await _orderRepository.ModelIdAsync(id);
          
            await _orderRepository.ModelDeleteAsync(order);
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id) => _orderRepository.ModelExist(id);


    }
}
