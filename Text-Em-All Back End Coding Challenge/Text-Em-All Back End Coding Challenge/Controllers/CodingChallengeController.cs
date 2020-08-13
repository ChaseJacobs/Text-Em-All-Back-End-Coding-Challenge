using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebApp.DataAccess;

namespace WebApp.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CodingChallengeController : ControllerBase
  {
    private readonly ILoggerManager _logger;
    private IRepository _repo;
    private JsonSerializerSettings _jsonSerializerSettings;

    public CodingChallengeController(ILoggerManager logger, IRepository repo)
    {
      _logger = logger;
      _repo = repo;
      DefaultContractResolver contractResolver = new DefaultContractResolver
      {
        NamingStrategy = new CamelCaseNamingStrategy()
      };
      _jsonSerializerSettings = new JsonSerializerSettings
      {
        ContractResolver = contractResolver,
        Formatting = Formatting.Indented
      };
    }

    [HttpGet]
    public string Welcome()
    {
      _logger.LogInfo("Enterned the Get 'welcome' Method.");

      return "Welcome to Chase Jacobs' Coding Challenge!";
    }

    [HttpGet]
    [Route("student/{studentId}/transcript")]
    public string GetStudent(int studentId)
    {
      _logger.LogInfo("Enterned the Get Student Method.");

      var student = _repo.GetStudent(studentId);

      var json = JsonConvert.SerializeObject(student, _jsonSerializerSettings);
      return json;
    }

    [HttpGet]
    [Route("students")]
    public string GetStudents()
    {
      _logger.LogInfo("Enterned the Get Students Method.");

      var students = _repo.GetStudents();

      var json = JsonConvert.SerializeObject(students, _jsonSerializerSettings);
      return json;
    }
  }
}
