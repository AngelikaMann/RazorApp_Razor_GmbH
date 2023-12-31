﻿using FirstRazorApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace FirstRazorApp.AppRepository
{
    public class MockEmploeeRepository : IEmpoyeeRepository
    {
        private List<Employee> _peopleList;

        public MockEmploeeRepository()
        {
            _peopleList = new List<Employee>()
            {
                new Employee()
                    {Id = 0, Name = "Mary", Email = "example@example.com", PotoPath = "avatar2.png", Department = Dept.Hr},
                new Employee()
                    {Id = 1, Name = "Mark", Email = "example1@example.com", PotoPath = "avatar.png", Department = Dept.It},
                new Employee()
                    {Id = 2, Name = "Kolyan", Email = "demonick@example.com", PotoPath = "avatar4.png", Department = Dept.It},
                new Employee()
                    {Id = 3, Name = "Shawn", Email = "example2@example.com", PotoPath = "avatar5.png", Department = Dept.Payroll},
                new Employee()
                    {Id = 4, Name = "Jeniffer", Email = "example3@example.com", PotoPath = "avatar3.png", Department = Dept.Hr},
                new Employee()
                    {Id = 5, Name = "Sten", Email = "example4@example.com", Department = Dept.Payroll}
            };
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _peopleList;
        }

        public Employee GetEmployee(int id)
        {
            return _peopleList.FirstOrDefault(x => x.Id == id);
        }

        public Employee Update(Employee updatedEmployee)
        {

            Employee employee = _peopleList.FirstOrDefault(x => x.Id == updatedEmployee.Id);


            if (employee != null)
            {
                employee.Name = updatedEmployee.Name;
                employee.Email = updatedEmployee.Email;
                employee.Department = updatedEmployee.Department;
                employee.PotoPath = updatedEmployee.PotoPath;
            }

            return employee;
        }


        public Employee Add(Employee newEmployee)
        {

            if (_peopleList.Count > 0)
                newEmployee.Id = _peopleList.Max(x => x.Id) + 1;
            else
                newEmployee.Id = 1;


            _peopleList.Add(newEmployee);


            return newEmployee;
        }

        public Employee Delete(int id)
        {
            Employee employeeToDelete = _peopleList.FirstOrDefault(x => x.Id == id);

            if (employeeToDelete != null)
                _peopleList.Remove(employeeToDelete);

            return employeeToDelete;
        }

        public IEnumerable<DeptHeadCount> EmployeeCountByDept(Dept? dept)
        {
            IEnumerable<Employee> query = _peopleList;

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


        public IEnumerable<Employee> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return _peopleList;
            }
            return _peopleList.Where(x => x.Name.ToLower().Contains(searchTerm) ||
                                          x.Email.ToLower().Contains(searchTerm));
        }
    }
}
