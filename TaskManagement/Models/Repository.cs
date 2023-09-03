using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TaskManagement.Models
{
    public class Repository : IRepository
    {
        private readonly TaskManagementDbContext _context;
        public Repository()
        {
            _context = new TaskManagementDbContext();
        }
        public void AssignTask(Task newTask)
        {
            var projectId = new SqlParameter("@ProjectId", newTask.ProjectId);
            var description = new SqlParameter("@Description", newTask.Description);
            var startDate = new SqlParameter("@StartDate", newTask.StartDate);
            var endDate = new SqlParameter("@EndDate", newTask.EndDate);
            var employeeIds = new SqlParameter("@EmployeeIds", string.Join(",", newTask.SelectedEmployees));
            _context.Database.ExecuteSqlCommand("AssignTask @Description, @StartDate, @EndDate, @ProjectId, @EmployeeIds", description, startDate, endDate, projectId, employeeIds);
        }

        public IEnumerable<Employee> GetEmployeesByProject(int projectId)
        {
            return _context.Employees.Where(e => e.ProjectId == projectId).ToList();
        }

        public IEnumerable<Project> GetProjects()
        {
            return _context.Projects.ToList();
        }

        public IEnumerable<Task> GetTasksByProject(int projectId)
        {
            var tasks = new List<Task>();
            if (projectId == 0)
            {
                tasks = _context.Tasks.ToList();
            }
            else
            {
                tasks = _context.Tasks.Where(t => t.ProjectId == projectId).ToList();
            }
            return tasks;
        }
    }
}