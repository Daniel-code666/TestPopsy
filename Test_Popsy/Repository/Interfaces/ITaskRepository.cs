using Test_Popsy.Models;

namespace Test_Popsy.Repository.Interfaces
{
    public interface ITaskRepository
    {
        Task<bool> CreateTask(TaskModel task);

        Task<bool> DeleteTask(TaskModel task);

        Task<bool> UpdateTask(TaskModel task);

        Task<TaskModel?> GetTaskById(int id);

        Task<ICollection<TaskModel>> GetAllTasks();

    }
}
