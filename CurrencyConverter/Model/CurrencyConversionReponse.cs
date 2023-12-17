namespace CurrencyConverter.Model
{
    public class CurrencyConversionReponse
    {
        public decimal exchangeRate { get; set; }
        public decimal convertedAmount { get; set; }

        public bool isValid { get; set; }

        public string validationMessage { get; set; }
    }
}
