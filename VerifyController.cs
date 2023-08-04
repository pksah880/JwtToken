using Home.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Home.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifyController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [Route("GetData")]

        public string GetData()
        {
            return "Authenticated with Jwt";
        }
       
        [HttpGet]
        [Route("detailsData")]

        public string detailsData()
        {
            return "Authenticated with Jwt";
        }
        [Authorize]
        [HttpPost]
       
        public string DetalisAdd(Users _user)
        {
            return "added";
        }
    }
}
