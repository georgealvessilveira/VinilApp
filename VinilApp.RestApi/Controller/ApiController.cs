using Microsoft.AspNetCore.Mvc;

namespace VinilApp.RestApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiController : ControllerBase { }
}