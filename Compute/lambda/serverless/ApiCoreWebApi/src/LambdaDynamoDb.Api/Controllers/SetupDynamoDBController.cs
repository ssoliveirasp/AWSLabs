using lambdaApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace LambdaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SetupDynamoDBController : ControllerBase
{
    public SetupDynamoDBController()
    {

    }

    [HttpGet("Online")]
    public ActionResult Online()
    {
        return Ok(OnlineService.Works);
    }

    // POST api/values
    [HttpPost("Dynamodb/CreateTable")]
    public void CreateTable()
    {

    }
}