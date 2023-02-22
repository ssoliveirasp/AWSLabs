using lambdaApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace LambdaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SetupDynamoDBController : ControllerBase
{
    [HttpGet("Online")]
    public ActionResult Online()
    {
        return Ok(OnlineService.Works);
    }

    // POST api/values
    [HttpPost("Setup")]
    public void SetupDB()
    {
    }
}