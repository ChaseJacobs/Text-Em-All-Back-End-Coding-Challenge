using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebApp.DataAccess;
using WebApp.DTOs;

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
    public IActionResult Welcome()
    {
      _logger.LogInfo("Enterned the Get 'welcome' Method.");

      return Ok("Welcome to Chase Jacobs' Coding Challenge!");
    }

    [HttpGet]
    [Route("student/{studentId}/transcript")]
    public IActionResult GetStudent(int studentId)
    {
      _logger.LogInfo("Enterned the Get Student Method.");

      var student = _repo.GetStudent(studentId);

      if (student == null)
      {
        return NotFound();
      }

      var json = JsonConvert.SerializeObject(student, _jsonSerializerSettings);
      return Ok(json);
    }

    [HttpGet]
    [Route("students")]
    public IActionResult GetStudents()
    {
      _logger.LogInfo("Enterned the Get Students Method.");

      var students = _repo.GetStudents();

      if(students == null)
      {
        return NotFound();
      }

      var json = JsonConvert.SerializeObject(students, _jsonSerializerSettings);
      return Ok(json);
    }

    [HttpPost]
    [Route("grades")]
    public IActionResult GetStudents([FromBody] NewGradeDTO grade)
    {
      _logger.LogInfo("Enterned the Update Grade Method.");

      var students = _repo.AddStudentGrade(grade);

      var json = JsonConvert.SerializeObject(students, _jsonSerializerSettings);
      return Ok(json);
    }
  }
}
