using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TheLogoPhilia.ApplicationEnums;

namespace TheLogoPhilia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnumsController : ControllerBase
    {
        [HttpGet("GetGenders")]
        public IActionResult GetGenders()
        {
            var genders = Enum.GetValues(typeof(Gender)).Cast<int>().ToList();
            List<string> gender = new List<string>();
            foreach (var item in genders)
            {
                gender.Add(Enum.GetName(typeof(Gender),item));
            }
            return Ok(gender);

        }
        [HttpGet("GetMessageTypes")]
        public IActionResult GetMessageTypes()
        {
            var genders = Enum.GetValues(typeof(MessageType)).Cast<int>().ToList();
            List<string> messageTypes = new List<string>();
            foreach (var item in genders)
            {
                messageTypes.Add(Enum.GetName(typeof(MessageType),item));
            }
            return Ok(messageTypes);

        }
        [HttpGet("GetCompetitionTypes")]
        public IActionResult GetCompetitionTypes()
        {
            var compTypes = Enum.GetValues(typeof(CompetitionType)).Cast<int>().ToList();
            List<string> CompetitionTypes = new List<string>();
            foreach (var item in compTypes)
            {
                CompetitionTypes.Add(Enum.GetName(typeof(CompetitionType),item));
            }
            return Ok(CompetitionTypes);

        }


    }
}