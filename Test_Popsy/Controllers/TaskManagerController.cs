using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Test_Popsy.Models;
using Test_Popsy.Repository.Interfaces;
using Test_Popsy.Utils;

namespace Test_Popsy.Controllers
{
    [Route("api/taskmanager")]
    [ApiController]
    public class TaskManagerController(ITaskRepository taskRepo) : ControllerBase
    {
        private readonly ITaskRepository _taskRepository = taskRepo;

        [HttpPost("create_task")]
        public async Task<IActionResult> CreateTask(TaskModel task)
        {
            try
            {
                if(!ModelState.IsValid || task == null)
                {
                    return StatusCode(400, new ApiResponse<string>("Bad request", ""));
                } 

                if (!await _taskRepository.CreateTask(task))
                {
                    return StatusCode(500, new ApiResponse<string>("Error", ""));
                }

                return Ok(new ApiResponse<TaskModel>("Task created", task));

            } catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>(ex.Message, ""));
            }
        }

        [HttpGet("get_tasks")]
        public async Task<IActionResult> GetTasks()
        {
            try
            {
                return Ok(new ApiResponse<IEnumerable<TaskModel>>("Ok", await _taskRepository.GetAllTasks()));
            }  catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<List<TaskModel>>(ex.Message, []));
            }
        }

        [HttpDelete("delete_task")]
        public async Task<IActionResult> DeleteTask(TaskModel task)
        {
            try
            {
                if (!ModelState.IsValid || task == null)
                {
                    return StatusCode(400, new ApiResponse<string>("Bad request", ""));
                }

                if(!await _taskRepository.DeleteTask(task))
                {
                    return StatusCode(500, new ApiResponse<string>("Error", ""));
                }

                return Ok(new ApiResponse<TaskModel>("Task deleted", task));
            } catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>(ex.Message, ""));
            }
        }

        [HttpPatch("update_task")]
        public async Task<IActionResult> UpdateTask([FromQuery] int id)
        {
            try
            {
                var foundTask = await _taskRepository.GetTaskById(id);

                if (foundTask == null)
                {
                    return StatusCode(404, new ApiResponse<string>("Task not found", ""));
                }

                if (!await _taskRepository.UpdateTask(foundTask))
                {
                    return StatusCode(500, new ApiResponse<string>("Error", ""));
                }

                return Ok(new ApiResponse<string>("Task updated", ""));

            } catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>(ex.Message, ""));
            }
        }
    }
}
