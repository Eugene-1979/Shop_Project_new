﻿using Microsoft.EntityFrameworkCore;
using Shop_Project.Db;
using Shop_Project.Models;

namespace Shop_Project.Repository
    {
    public class EmployeeRepository : IReposytory<Employee>
        {

        internal readonly AppDbContent _context;

        public EmployeeRepository(AppDbContent context)
            {
            _context = context;
            }

       async public Task ModelAddAsync(Employee model)
            {
            _context.Add(model);
            await _context.SaveChangesAsync();
            }

        async public Task<IEnumerable<Employee>> ModelAllAsync() => await _context.Employees.ToListAsync();



       async public Task ModelDeleteAsync(Employee model)
            {
            if(model!= null)
                {
                _context.Employees.Remove(model);
                }

            await _context.SaveChangesAsync();
            }

        public bool ModelExist(int id)=> (_context.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
            

      async public Task<Employee> ModelFirstofDefaultAsync(int? id)=> await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);


      async  public Task<Employee> ModelIdAsync(int? id)=> await _context.Employees.FindAsync(id);


      async  public Task ModelUpdateAsync(Employee model)
            {
            _context.Update(model);
            await _context.SaveChangesAsync();
            }
        }
    }
