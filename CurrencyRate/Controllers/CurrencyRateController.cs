using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using System.Threading.Tasks;

namespace CurrencyRate.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class CurrencyRateController : ControllerBase
    {

        private readonly CurrencyRateService _currencyRateService;

        public CurrencyRateController(CurrencyRateService currencyRateService)
        {
            _currencyRateService = currencyRateService;
        }

        [HttpGet("banks/{datePicker?}")]
        public async Task<ActionResult> Get(DateTime datePicker)
        {
            var allData = await _currencyRateService.GetCurrencyRate(datePicker.Date);

            return Ok();
        }
    }
}
