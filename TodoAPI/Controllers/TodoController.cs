using Microsoft.AspNetCore.Mvc;
using TodoAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoAPI.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private MsglowBelajarContext _msglowBelajarContext;

        public TodoController(MsglowBelajarContext msglowBelajarContext)
        {
            _msglowBelajarContext = msglowBelajarContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoModel>> Get()
        {
            return _msglowBelajarContext.Todos.ToList();
        }

        // GET api/<TodoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
        public void Delete(int id)
        {
        }

        ~TodoController ()
        {
            _msglowBelajarContext.Dispose();
        }
    }
}
