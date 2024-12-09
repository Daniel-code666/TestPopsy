using Microsoft.EntityFrameworkCore;
using Test_Popsy.Data;
using Test_Popsy.Models;
using Test_Popsy.Repository.Interfaces;

namespace Test_Popsy.Repository.ImplementClass
{
    public class TaskRepository(AppDbContext db) : ITaskRepository
    {
        private readonly AppDbContext _db = db;

        public async Task<bool> CreateTask(TaskModel task)
        {
            try
            {
                DateTime fechaActual = DateTime.Now;

                if (task.FechaCreacion > fechaActual)
                {
                    return false;
                }

                await _db.TaskModel.AddAsync(task);

                return await Save();
            } catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteTask(TaskModel task)
        {
            try
            {
                var found_task = await _db.TaskModel.FirstOrDefaultAsync(t => t.Id == task.Id);

                _db.TaskModel.Remove(found_task!);

                return await Save();

            } catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<TaskModel?> GetTaskById(int id)
        {
            try
            {
                return await _db.TaskModel.FirstOrDefaultAsync(t => t.Id == id);
            } catch (Exception ex)
            {
                return new TaskModel();
            }
        }

        public async Task<ICollection<TaskModel>> GetAllTasks()
        {
            try
            {
                return await _db.TaskModel.ToListAsync();
            } catch (Exception ex)
            {
                return new List<TaskModel>();
            }
        }

        public async Task<bool> UpdateTask(TaskModel task)
        {
            try
            {
                var existingTask = await _db.TaskModel.FindAsync(task.Id);

                existingTask.Titulo = task.Titulo;
                existingTask.Descripcion = task.Descripcion;
                existingTask.FechaVencimiento = task.FechaVencimiento;
                existingTask.Estado = task.Estado;

                // Guardar los cambios en la base de datos
                return await Save();
            } catch (Exception ex)
            {
                return false;
            }
        }

        private async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() >= 0;
        }
    }
}
