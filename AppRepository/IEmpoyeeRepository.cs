using FirstRazorApp.Models;
using System.Collections.Generic;

namespace FirstRazorApp.AppRepository
{
    public interface IEmpoyeeRepository
    {
        IEnumerable<Employee> Search(string searchTerm);
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployee(int id);
        Employee Update(Employee updatedEmployee);
        Employee Add(Employee newEmployee);
        Employee Delete(int id);

        // Mengenverfolgung
        IEnumerable<DeptHeadCount> EmployeeCountByDept(Dept? dept);
    }
}
