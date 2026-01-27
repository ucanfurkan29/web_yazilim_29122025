using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace _18_filter_operation.Filters
{
    public class PerformansFiltresiAttribute : ActionFilterAttribute
    {

        //Metot başlamadan hemen önce çalışır
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Action metodundan önce çalışacak kodlar
            var stopwatch = Stopwatch.StartNew(); // Kronometreyi başlat
            context.HttpContext.Items["Kronometre"] = stopwatch; // Kronometreyi HttpContext'e ekle

            base.OnActionExecuting(context);
        }

        //Metot tamamlandıktan hemen sonra çalış
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // Action metodundan sonra çalışacak kodlar
            if (context.HttpContext.Items["Kronometre"] is Stopwatch stopwatch)
            {
                stopwatch.Stop();
                var gecenSure = stopwatch.ElapsedMilliseconds;

                if (context.Controller is Controller controllerNesnesi)
                {
                    controllerNesnesi.ViewBag.PerformansBilgisi = $"İşlem Süresi: {gecenSure} ms";
                }

                var controller = context.RouteData.Values["controller"];
                var action = context.RouteData.Values["action"];

                string logMesaji = $"[PERFORMANS] {controller}/{action} : {gecenSure} ms sürdü";
                Debug.WriteLine(logMesaji);

                context.HttpContext.Response.Headers.Add("X-Islem-Suresi",$"{gecenSure}ms");
            }
            base.OnActionExecuted(context);
        }


    }
}
