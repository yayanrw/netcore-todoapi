using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Helper;
using TodoAPI.Models;
using TodoAPI.Services;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private MsglowBelajarContext _db;
        private readonly IUriService uriService;

        public MemberController(MsglowBelajarContext msglowBelajarContext, IUriService uriService)
        {
            _db = msglowBelajarContext;
            this.uriService = uriService;
        }

        [HttpGet]
        public async Task<ActionResult<MemberModel>> Get([FromQuery] PaginationFilter filter)
        {
            try
            {
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = await _db.Member
                    .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                    .ToListAsync();
                var totalRecords = await _db.Member.CountAsync();
                var pagedReponse = PaginationHelper.CreatePagedReponse<MemberModel>(pagedData, validFilter, totalRecords, uriService, route);
                return Ok(pagedReponse);
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
    }
}
