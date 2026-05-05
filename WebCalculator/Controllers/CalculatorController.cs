using Microsoft.AspNetCore.Mvc;
using WebCalculator.Models;
using WebCalculator.Services;

namespace WebCalculator.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CalculatorController : ControllerBase
{
    private readonly NativeCalculatorService _calculator;

    public CalculatorController(NativeCalculatorService calculator)
    {
        _calculator = calculator;
    }

    [HttpPost("calculate")]
    public ActionResult<CalculationResponse> Calculate([FromBody] CalculationRequest request)
    {
        try
        {
            var result = _calculator.Calculate(request.Left, request.Right, request.Operator);

            if (double.IsNaN(result) || double.IsInfinity(result))
            {
                return BadRequest(new CalculationResponse
                {
                    Error = "Operazione non valida (controlla divisione per zero)."
                });
            }

            return Ok(new CalculationResponse { Result = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new CalculationResponse { Error = ex.Message });
        }
    }
}
