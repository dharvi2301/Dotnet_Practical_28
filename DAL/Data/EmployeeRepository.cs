using DAL.Models.DTO;
using DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class EmployeeRepository
    {
        public void Create(AddEmployeeDTO emp)
        {
            using (var conn = DbService.Instance.GetConnection())
            {
                string query = @"INSERT INTO Employee 
                                (EmployeeName, EmployeeSalary, DepartmentId, EmployeeEmail, EmployeeJoiningDate, EmployeeStatus)
                                 VALUES (@Name, @Salary, @DeptId, @Email, @JoinDate, 'Active')";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", emp.EmployeeName);
                cmd.Parameters.AddWithValue("@Salary", emp.EmployeeSalary);
                cmd.Parameters.AddWithValue("@DeptId", emp.DepartmentId);
                cmd.Parameters.AddWithValue("@Email", emp.EmployeeEmail);
                cmd.Parameters.AddWithValue("@JoinDate", emp.EmployeeJoiningDate);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Employee emp)
        {
            using (var conn = DbService.Instance.GetConnection())
            {
                string query = @"UPDATE Employee SET 
                                EmployeeName = @Name,
                                EmployeeSalary = @Salary,
                                DepartmentId = @DeptId,
                                EmployeeEmail = @Email
                                WHERE EmployeeId = @Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", emp.EmployeeId);
                cmd.Parameters.AddWithValue("@Name", emp.EmployeeName);
                cmd.Parameters.AddWithValue("@Salary", emp.EmployeeSalary);
                cmd.Parameters.AddWithValue("@DeptId", emp.DepartmentId);
                cmd.Parameters.AddWithValue("@Email", emp.EmployeeEmail);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void SoftDelete(int id)
        {
            using (var conn = DbService.Instance.GetConnection())
            {
                string query = "UPDATE Employee SET EmployeeStatus = 'InActive' WHERE EmployeeId = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Employee GetById(int id)
        {
            Employee emp = null;
            using (var conn = DbService.Instance.GetConnection())
            {
                string query = "SELECT * FROM Employee WHERE EmployeeId = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    emp = new Employee
                    {
                        EmployeeId = (int)reader["EmployeeId"],
                        EmployeeName = reader["EmployeeName"].ToString(),
                        EmployeeSalary = (decimal)reader["EmployeeSalary"],
                        DepartmentId = (int)reader["DepartmentId"],
                        EmployeeEmail = reader["EmployeeEmail"].ToString(),
                        EmployeeJoiningDate = (DateTime)reader["EmployeeJoiningDate"],
                        EmployeeStatus = reader["EmployeeStatus"].ToString()
                    };
                }
            }
            return emp;
        }

        public List<Employee> GetAll()
        {
            List<Employee> list = new List<Employee>();
            using (var conn = DbService.Instance.GetConnection())
            {
                string query = "SELECT * FROM Employee";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Employee
                    {
                        EmployeeId = (int)reader["EmployeeId"],
                        EmployeeName = reader["EmployeeName"].ToString(),
                        EmployeeSalary = (decimal)reader["EmployeeSalary"],
                        DepartmentId = (int)reader["DepartmentId"],
                        EmployeeEmail = reader["EmployeeEmail"].ToString(),
                        EmployeeJoiningDate = (DateTime)reader["EmployeeJoiningDate"],
                        EmployeeStatus = reader["EmployeeStatus"].ToString()
                    });
                }
            }

            if (list.Count > 0)
            {
                var lastName = list[^1].EmployeeName; // C# 13: implicit ^0 indexing
                new FileLoggerService().Log($"Last employee in list: {lastName}");
            }

            return list;
        }

    }
}

