using FirstRazorApp.AppRepository;
using FirstRazorApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace FirstRazorApp.Services
{
    public class SQLEmployeeRepository : IEmpoyeeRepository
    {
        private readonly AppDbContext _context;

        public SQLEmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return _context.Employees;
            }
            return _context.Employees.Where(x => x.Name.Contains(searchTerm) ||
                                          x.Email.Contains(searchTerm));
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            //return _context.Employees;
            return _context.Employees
            .FromSqlRaw<Employee>("Select * from Employees")
                .ToList();


        }

        public Employee GetEmployee(int id)
        {
            //return _context.Employees.FirstOrDefault(x => x.Id == id);
            return _context.Employees
                .FromSqlRaw<Employee>("CodeFirstSPGetEmployeeById {0}", id)
                .ToList()
                .FirstOrDefault();
        }

        public Employee Update(Employee updatedEmployee)
        {
            var employee = _context.Employees.Attach(updatedEmployee);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return updatedEmployee;
        }

        public Employee Add(Employee newEmployee)
        {
            //_context.Employees.Add(newEmployee);
            //_context.SaveChanges();
            //return newEmployee;

            //insert,Update,delete-executeSQLRaw
            _context.Database.ExecuteSqlRaw("spAddNewExployee {0},{1},{2},{3}",
                newEmployee.Name,
                newEmployee.Email,
                newEmployee.PotoPath,
                newEmployee.Department);
            return newEmployee;
        }

        public Employee Delete(int id)
        {
            Employee employee = _context.Employees.Find(id);

            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }

            return employee;
        }

        public IEnumerable<DeptHeadCount> EmployeeCountByDept(Dept? dept)
        {
            IEnumerable<Employee> query = _context.Employees;

            if (dept.HasValue)
            {
                query = query.Where(e => e.Department == dept.Value);
            }

            return query.GroupBy(x => x.Department)
                .Select(p => new DeptHeadCount()
                {
                    Department = p.Key.Value,
                    Count = p.Count()
                }).ToList();
        }
    }
}
