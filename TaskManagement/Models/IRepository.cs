using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Models
{
    public interface IRepository
    {
        IEnumerable<Project> GetProjects();
        IEnumerable<Employee> GetEmployeesByProject(int projectId);
        IEnumerable<Task> GetTasksByProject(int projectId);
        void AssignTask(Task newTask);
    }
}
