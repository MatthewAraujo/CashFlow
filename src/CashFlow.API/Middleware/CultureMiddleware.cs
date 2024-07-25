using System.Globalization;

namespace CashFlow.API.Middleware
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;

        public CultureMiddleware(RequestDelegate next) 
        { 
            _next = next;
        }
        public async Task Invoke(HttpContext context) 
        {
     
            var supportedLaqnguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

            var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

            
            var culture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

            var cultureInfo = new CultureInfo("en");

            if (
                string.IsNullOrWhiteSpace(culture) == false 
                && supportedLaqnguages.Exists(l => l.Name.Equals(requestedCulture))) 
            {
                cultureInfo = new CultureInfo(culture);
            }

            CultureInfo.CurrentCulture = cultureInfo;
            
            await _next(context);
        
        }
    }
}
