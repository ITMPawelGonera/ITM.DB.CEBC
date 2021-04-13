using ITM.DB.CEBC.Api.Logic;
using ITM.DB.CEBC.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITM.DB.CEBC.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanController : ControllerBase
    {
        ApiConfig _config;
        LoanCalculator _calc;
        public LoanController(IOptions<ApiConfig> config, LoanCalculator calc) {
            _config = config.Value;
            _calc = calc;
        }
        [HttpPost]
        public IActionResult BorrowTheLoan (LoanInputModel postModel)
        {
            if (postModel.LoanAmount == 0) throw new ArgumentOutOfRangeException("LoanAmount");
            if (postModel.LoanDurationMonths == 0 && postModel.LoanDurationYears == 0) throw new ArgumentOutOfRangeException("LoanDuration");
            LoanModel m = _calc.CalcLoan(postModel);
            var r = new LoanOutputModel()
            {
                MonthlyCost = m.MonthlyCost,
                TotalInterest = m.TotalInterest,
                TotalFees = m.TotalFee,
                APR = m.APR
            };
            return new JsonResult(r);
        }
    }
}
