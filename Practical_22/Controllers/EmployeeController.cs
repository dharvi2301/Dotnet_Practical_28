using DAL.Data;
using DAL.Models;
using DAL.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Practical_22.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository _repo;
        private readonly ILoggerService _logger;

        // Inject both repository and logger via constructor
        public EmployeeController(EmployeeRepository repo, ILoggerService logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Employee emp = _repo.GetById(id);
            _logger.Log($"Fetched employee with ID {id}");
            return emp == null ? NotFound() : Ok(emp);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Employee> employees = _repo.GetAll();
            _logger.Log("Fetched all employees");
            return Ok(employees);
        }

        [HttpPost]
        public IActionResult Create(AddEmployeeDTO emp)
        {
            _repo.Create(emp);
            _logger.Log($"Created employee {emp.EmployeeName}");
            return Ok("Employee added");
        }

        [HttpPut]
        public IActionResult Update(Employee emp)
        {
            _repo.Update(emp);
            _logger.Log($"Updated employee ID {emp.EmployeeId}");
            return Ok("Employee updated");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repo.SoftDelete(id);
            _logger.Log($"Soft deleted employee ID {id}");
            return Ok("Employee soft deleted");
        }
    }
}
