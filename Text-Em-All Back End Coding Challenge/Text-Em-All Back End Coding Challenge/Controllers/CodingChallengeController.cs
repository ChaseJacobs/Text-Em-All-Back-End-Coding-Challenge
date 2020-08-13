using Database;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Text_Em_All_Back_End_Coding_Challenge.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CodingChallengeController : ControllerBase
  {
    private readonly ILoggerManager _logger;
    private IRepository _repo;

    public CodingChallengeController(ILoggerManager logger, IRepository repo)
    {
      _logger = logger;
      _repo = repo;
    }

    [HttpGet]
    public string Welcome()
    {
      _logger.LogInfo("Enterned the Get 'welcome' Method.");

      return "Welcome to Chase Jacobs' Coding Challenge!";
    }

    [HttpGet]
    [Route("Person/{id}")]
    public string GetPerson(int id)
    {
      _logger.LogInfo("Enterned the Get Person Method.");


      var student = _repo.GetPerson(id);


      var json = JsonConvert.SerializeObject(student);
      return json;
    }
  }
}
