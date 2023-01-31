﻿using Microsoft.EntityFrameworkCore;
using Shop_Project.Db;
using Shop_Project.Models;

namespace Shop_Project.Repository
    {
    public class CustomerRepository : IReposytory<Customer>
        {
       internal readonly AppDbContent _context;

        public CustomerRepository(AppDbContent context)
            {
            _context = context;
            }




       async public Task ModelAddAsync(Customer model)
            {
            _context.Add(model);
            await _context.SaveChangesAsync();
            }



        async public Task<IEnumerable<Customer>> ModelAllAsync() => await _context.Customers.ToListAsync();



      async  public Task ModelDeleteAsync(Customer model)
            {
            if(model != null)
                {
                _context.Customers.Remove(model);
                }

            await _context.SaveChangesAsync();
            }

        public bool ModelExist(int id) => (_context.Customers?.Any(e => e.Id == id)).GetValueOrDefault();
           

       async public Task<Customer> ModelFirstofDefaultAsync(int? id) => await _context.Customers.FirstOrDefaultAsync(m => m.Id == id);


      async public Task<Customer> ModelIdAsync(int? id)=> await _context.Customers.FindAsync(id);
         

       async public Task ModelUpdateAsync(Customer model)
            {
            _context.Update(model);
            await _context.SaveChangesAsync();
            }

       
        }
    }
