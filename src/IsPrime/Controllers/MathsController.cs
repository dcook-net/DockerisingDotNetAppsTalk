using System;
using Microsoft.AspNetCore.Mvc;

namespace IsPrime.Controllers
{
    [ApiController]
    public class MathsController : Controller
    {
        [HttpGet("IsPrime/{number}")]
        public IActionResult Get([FromRoute] int number)
        {
            return Ok(IsPrime(number));
        }

        public bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (var i = 3; i <= boundary; i+=2)
                if (number % i == 0)
                    return false;

            return true;        
        }
    }
}