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
      //initialize logger for debug logging
      _logger = logger;

      //repo to access the database
      _repo = repo;

      //json serializer to show the proper camel case
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
      try
      {
        _logger.LogInfo("Entered the Get 'welcome' Method.");

        return Ok("Welcome to Chase Jacobs' Coding Challenge!");
      }
      catch (System.Exception e)
      {
        _logger.LogError("Error in Welcome " + e.ToString());
        throw;
      }
    }

    [HttpGet]
    [Route("student/{studentId}/transcript")]
    public IActionResult GetStudent(int studentId)
    {
      try
      {
        _logger.LogInfo("Entered the Get Student Method.");

        var student = _repo.GetStudent(studentId);

        if (student == null)
        {
          return NotFound();
        }

        var json = JsonConvert.SerializeObject(student, _jsonSerializerSettings);
        return Ok(json);
      }
      catch (System.Exception e)
      {
        _logger.LogError("Error in GetStudent " + e.ToString());
        throw;
      }
    }

    [HttpGet]
    [Route("students")]
    public IActionResult GetStudents()
    {
      try
      {
        _logger.LogInfo("Entered the Get Students Method.");

        var students = _repo.GetStudents();

        if (students == null)
        {
          return NotFound();
        }

        var json = JsonConvert.SerializeObject(students, _jsonSerializerSettings);
        return Ok(json);
      }
      catch (System.Exception e)
      {
        _logger.LogError("Error in GetStudents " + e.ToString());
        throw;
      }
    }

    [HttpPost]
    [Route("grades")]
    public IActionResult AddUpdateGrade([FromBody] NewGradeDTO grade)
    {
      try
      {
        _logger.LogInfo("Entered the Update Grade Method.");

        var newGrade = _repo.AddUpdateStudentGrade(grade);

        if (newGrade == null)
        {
          return BadRequest();
        }

        var json = JsonConvert.SerializeObject(newGrade, _jsonSerializerSettings);
        return Ok(json);
      }
      catch (System.Exception e)
      {
        _logger.LogError("Error in AddUpdateGrade " + e.ToString());
        throw;
      }
    }
  }
}
