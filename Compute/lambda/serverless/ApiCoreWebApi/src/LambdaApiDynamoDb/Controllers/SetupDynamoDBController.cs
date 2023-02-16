using Microsoft.AspNetCore.Mvc;

namespace LambdaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SetupDynamoDBController : ControllerBase
{
    [HttpGet("Online")]
    public ActionResult Online()
    {
        return Ok($"works {DateTime.Now}");
    }

    // POST api/values
    [HttpPost("Setup")]
    public void SetupDB()
    {
    }

    // // PUT api/values/5
    // [HttpPut("{id}")]
    // public void Put(int id, [FromBody]string value)
    // {
    // }

    // // DELETE api/values/5
    // [HttpDelete("{id}")]
    // public void Delete(int id)
    // {
    // }
}