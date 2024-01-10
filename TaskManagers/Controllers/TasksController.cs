using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagers.Models;

namespace TaskManagers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITasks _tasks;

        
        public TasksController(ITasks tasks)
        {
            _tasks = tasks;
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetTask()
        {
            return Ok(_tasks.GetTasks());
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("{id}", Name = "getTask")]

        public ActionResult GetTaskById (int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();

                }
                return Ok(_tasks.GetTaskById(id));
            }catch (Exception e)
            {
                return StatusCode(500, $"internal server error {e.Message}");
            }
            

        }

        [Authorize]
        [HttpPost]

        public IActionResult CreateTask(Tasks task)
        {
            try
            {
                if (task.Task_Id > 0)
                {
                    return BadRequest();
                }
                task.Task_Id = _tasks.GetTasks().OrderByDescending(s => s.Task_Id).FirstOrDefault().Task_Id + 1;

                _tasks.CreateTask(task);
                return CreatedAtRoute("getTask", new { id = task.Task_Id }, task);
            }catch (Exception e)
            {
                return StatusCode(500, $"internal server error {e.Message}");
            }
            
        }

        [Authorize]
        [HttpPut("{id}")]

        public IActionResult UpadateTask (int id,  Tasks task )
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest();
                }
                var existingTask = _tasks.GetTasks().FirstOrDefault(s => s.Task_Id == id);
                if(existingTask == null)
                {
                    return BadRequest();
                }

                _tasks.UpdateTask(id, task);
                return NoContent();
            }catch (Exception e)
            {
                return StatusCode(500, $"internal server error {e.Message}");
            }
        }


        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{id}")]

        public IActionResult DeleteTask (int id)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest();
                }
                _tasks.DeleteTask(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"internal server error {e.Message}");
            }
        }

    }
}
