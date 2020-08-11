using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Text_Em_All_Back_End_Coding_Challenge.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private readonly ILoggerManager _logger;
    private IRepositoryWrapper _repoWrapper;

    public WeatherForecastController(ILoggerManager logger, IRepositoryWrapper repoWrapper)
    {
      _logger = logger;
      _repoWrapper = repoWrapper;
    }

    [HttpGet]
    public string Get()
    {
      _logger.LogInfo("Enterned the Get Method.");

      var student = _repoWrapper.Person.FindByCondition(p => p.PersonId == 4).FirstOrDefault();


      var json = JsonConvert.SerializeObject(student);
      return json;
    }
  }
}
