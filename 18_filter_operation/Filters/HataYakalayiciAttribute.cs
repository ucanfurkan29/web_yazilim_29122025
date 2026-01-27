using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace _18_filter_operation.Filters
{
    public class HataYakalayiciAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            string hataMesaji = context.Exception.Message;
            Console.WriteLine($"[ACİL DURUM] Hata Oluştu: {hataMesaji}");

            var result = new ViewResult{ ViewName = "OzelHataSayfasi" };

            result.ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
            result.ViewData["HataMesaji"] = hataMesaji;
            context.Result = result;
            context.ExceptionHandled = true;
            base.OnException(context);
        }
    }
}
