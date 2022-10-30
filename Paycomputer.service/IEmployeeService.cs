using Microsoft.AspNetCore.Mvc.Rendering;
using Paycompute.Entity;


namespace Paycomputer.service
{
    public interface IEmployeeService
    {
        Task CreateAsync(Employee newEmployee);
        Employee GetById(int employeeId);
        Task UpdateAsync(Employee employee);
        Task UpdateAsync(int id);
        Task Delete(int employeeId);
        decimal UnionFees(int id);
        decimal StudentLoanRepaymentAmount(int id, decimal totalAmount);
        IEnumerable<SelectListItem> GetAllEmployeesForPayroll();
        IEnumerable<Employee> GetAll();
    }
}
