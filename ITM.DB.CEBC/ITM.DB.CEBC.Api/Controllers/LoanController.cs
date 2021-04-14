using ITM.DB.CEBC.Api.Logic;
using ITM.DB.CEBC.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ITM.DB.CEBC.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanController : ControllerBase
    {
        ApiConfig _config;
        LoanCalculator _calc;
        public LoanController(IOptions<ApiConfig> config, LoanCalculator calc) 
        {
            _config = config.Value;
            _calc = calc;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult BorrowTheLoan (LoanInputModel postModel)
        {
            try
            {
                if (postModel.LoanAmount <= 0)
                {
                    throw new ArgumentOutOfRangeException("LoanAmount");
                }

                if (postModel.LoanDurationYears < 0)
                {
                    throw new ArgumentOutOfRangeException("LoanDurationYears");
                }

                if (postModel.LoanDurationMonths < 0 )
                {
                    throw new ArgumentOutOfRangeException("LoanDurationMonths");
                }

                if (postModel.LoanDurationMonths == 0 && postModel.LoanDurationYears == 0)
                {
                    throw new ArgumentOutOfRangeException("LoanDuration");
                }

                LoanModel m = _calc.CalcLoan(postModel);
                var r = new LoanOutputModel()
                {
                    MonthlyCost = Math.Round(m.MonthlyCost,4),
                    TotalInterest = Math.Round(m.TotalInterest, 4),
                    TotalFees = Math.Round(m.TotalFee, 4),
                    APR = Math.Round(m.APR*100,4)
                };
                return new JsonResult(r);
            }
            catch (ArgumentOutOfRangeException aex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, aex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
