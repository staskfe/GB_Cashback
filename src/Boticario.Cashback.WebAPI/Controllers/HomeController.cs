using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boticario.Cashback.WebAPI.Controllers
{
    [ApiController]
    [Route("home")]
    public class HomeController : BaseControllerCustom
    {
       
        [AllowAnonymous]
        [HttpGet]
        public object Get()
        {
            return new { message = "Api online!" };
        }

    }
}
