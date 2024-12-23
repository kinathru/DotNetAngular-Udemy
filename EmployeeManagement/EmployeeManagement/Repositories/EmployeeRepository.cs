using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories;

public class EmployeeRepository(AppDbContext context) : IEmployeeRepository
{
    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        return await context.Employees.ToListAsync();
    }

    public async Task<Employee?> GetEmployeeByIdAsync(int id)
    {
        return await context.Employees.FindAsync(id);
    }

    public async Task AddEmployeeAsync(Employee employee)
    {
        await context.Employees.AddAsync(employee);
        await context.SaveChangesAsync();
    }

    public async Task UpdateEmployeeAsync(Employee employee)
    {
        context.Employees.Update(employee);
        await context.SaveChangesAsync();
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        var employeeInDb = await context.Employees.FindAsync(id);
        if (employeeInDb == null)
        {
            throw new KeyNotFoundException($"Employee with id: {id} does not exist");
        }

        context.Employees.Remove(employeeInDb);
        await context.SaveChangesAsync();
    }
}