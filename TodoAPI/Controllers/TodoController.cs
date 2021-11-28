using Microsoft.AspNetCore.Mvc;
using TodoAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoAPI.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private MsglowBelajarContext _db;

        public TodoController(MsglowBelajarContext db)
        {
            _db = db;
        }

        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                var data = _db.Todos.ToList();
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

        // GET api/<TodoController>/5
        [HttpGet("{id}")]
        public JsonResult Get(String pid)
        {
            try
            {
                var data = _db.Todos.Where(x => x.pid == pid).FirstOrDefault();
                return new JsonResult(new
                {
                    status = true,
                    data = data,
                    messages = "Success"
                });
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

        // POST api/<TodoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TodoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TodoController>/5
        [HttpDelete("{id}")]
        public JsonResult Delete(String pid)
        {
            try
            {
                var data = _db.Todos.Where(x => x.pid == pid).FirstOrDefault();

                if (data != null)
                {
                    _db.Todos.Remove(data);
                    _db.SaveChanges();
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
