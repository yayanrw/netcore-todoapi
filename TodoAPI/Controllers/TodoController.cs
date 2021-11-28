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

        public TodoController(MsglowBelajarContext db)
        {
            _db = db;
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
                return new JsonResult(new { 
                    status = false,
                    messages = "Failed",
                    data = ex.Message.ToString()
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
                        status = true,
                        data = data,
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
                    code = ex.HResult,
                    messages = ex.Message.ToString()
                });
            }
        }

        [HttpPost]
        public async void Post([FromBody] TodoModel todoModel)
        {
            _db.Todos.Add(todoModel);
            await _db.SaveChangesAsync();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
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
                        status = true,
                        messages = "Data not found"
                    });
                }                
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = false,
                    code = ex.HResult,
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
