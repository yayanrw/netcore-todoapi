using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private MsglowBelajarContext _db;

        public TodoController(MsglowBelajarContext msglowBelajarContext)
        {
            _db = msglowBelajarContext;
        }

        [HttpGet]
        public async Task<ActionResult<TodoModel>> Get()
        {
            try
            {
                var data = await _db.Todos.ToListAsync();
                return new JsonResult(new { 
                    status = true, 
                    messages = "Success", 
                    data = data
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = false,
                    messages = ex.Message.ToString()
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoModel>> Get(String pid)
        {
            try
            {
                var data = await _db.Todos.FirstOrDefaultAsync(x => x.pid == pid);
                if (data == null)
                {
                    return new JsonResult(new
                    {
                        status = false,
                        messages = "Data not found"
                    });
                } 
                else
                {
                    return new JsonResult(new
                    {
                        status = true,
                        data = data,
                        messages = "Success"
                    });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = false,
                    messages = ex.Message.ToString()
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<TodoModel>> Post([FromBody] TodoModel todoModel)
        {
            try
            {
                todoModel.pid = Guid.NewGuid().ToString();
                todoModel.created_at = DateTime.Now;
                todoModel.created_by = "Anon";
                _db.Todos.Add(todoModel);
                await _db.SaveChangesAsync();
                return new JsonResult(new
                {
                    status = true,
                    data = todoModel,
                    messages = "Inserted Successfully"
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = false,
                    messages = ex.Message.ToString()
                });
            }

        }

        [HttpPut]
        public async Task<ActionResult<TodoModel>> Put([FromBody] TodoModel todoModel)
        {
            try
            {
                var data = await _db.Todos.FirstOrDefaultAsync(x => x.pid == todoModel.pid);
                if (data != null)
                {
                    data.title = todoModel.title;
                    data.description = todoModel.description;
                    data.date = todoModel.date;
                    data.updated_at = DateTime.Now;
                    data.updated_by = "Anon";

                    await _db.SaveChangesAsync();
                    return new JsonResult(new
                    {
                        status = true,
                        messages = "Updated Successfully"
                    });
                }
                else
                {
                    return new JsonResult(new
                    {
                        status = false,
                        messages = "Data not found"
                    });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = false,
                    messages = ex.Message.ToString()
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoModel>> Delete(String pid)
        {
            try
            {
                var data = await _db.Todos.FirstOrDefaultAsync(x => x.pid == pid);
                if (data != null)
                {
                    _db.Todos.Remove(data);
                    await _db.SaveChangesAsync();
                    return new JsonResult(new
                    {
                        status = true,
                        messages = "Deleted Successfully"
                    });
                }
                else
                {
                    return new JsonResult(new
                    {
                        status = false,
                        messages = "Data not found"
                    });
                }                
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = false,
                    messages = ex.Message.ToString()
                });
            }
        }

        ~TodoController ()
        {
            _db.Dispose();
        }
    }
}
