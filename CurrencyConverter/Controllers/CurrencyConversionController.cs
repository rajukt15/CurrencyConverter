using CurrencyConverter.BuisinessLayer;
using CurrencyConverter.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CurrencyConverter.Controllers
{
    [Route("api/[controller]/{sourceCurrency}/{targetCurrency}/{amount}")]
    [ApiController]
    public class CurrencyConversionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CurrencyConversionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet(Name = "Convert")]
        public CurrencyConversionReponse Convert(string sourceCurrency, string targetCurrency,
            decimal amount)
        {

            CurrencyConversionReponse currencyConversionReponse = new CurrencyConversionReponse();

            if (string.IsNullOrEmpty(sourceCurrency))
            {
                currencyConversionReponse.isValid = false;
                currencyConversionReponse.validationMessage = "Source Currency should not be blank";

                return currencyConversionReponse;
                
            }
            else if (string.IsNullOrEmpty(targetCurrency)){
                currencyConversionReponse.isValid = false;
                currencyConversionReponse.validationMessage = "Target Currency should not be blank";

                return currencyConversionReponse;
            }
            else if (amount<=0)
            {
                currencyConversionReponse.isValid = false;
                currencyConversionReponse.validationMessage = "Amount should be greater than 0";

                return currencyConversionReponse;
            }

            sourceCurrency = sourceCurrency.ToUpper();
                targetCurrency = targetCurrency.ToUpper();

                currencyConversionReponse = CurrencyConversionReponse(sourceCurrency + "_TO_" + targetCurrency, amount);

            

            return currencyConversionReponse;
        }

        public CurrencyConversionReponse CurrencyConversionReponse(string sourceToTarget, decimal amount)
        {
            CurrencyConversionReponse currency_Reponse = new CurrencyConversionReponse();
            var collection = _configuration.GetSection("exchangeValues").GetChildren();
            bool isInValidData = true;
            foreach (var item in collection)
            {
                if (item.Key == sourceToTarget)
                {
                    isInValidData = false;
                }
            }
            if (isInValidData)
            {
                currency_Reponse.isValid = false;
                currency_Reponse.validationMessage = "Entered Source to Target Convversion '"+sourceToTarget+"' not found";

                return currency_Reponse;
            }
            currency_Reponse.exchangeRate = decimal.Parse(_configuration.GetValue<string>("exchangeValues:" + sourceToTarget));
            currency_Reponse.convertedAmount = amount * currency_Reponse.exchangeRate;
            currency_Reponse.isValid = true;
            currency_Reponse.validationMessage = " Successful !";

            return currency_Reponse;
        }
    }
}
