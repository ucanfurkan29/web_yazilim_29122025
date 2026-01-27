using _18_filter_operation.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace _18_filter_operation.Filters
{
    //ActionFilterAttribute sınıfından türeyen bir filtre oluşturduk. Bu sınıf metodun tepesinde [KufurEngelleyici] şeklinde kullanılarak çalıştırılabilir.
    public class KufurEngelleyiciAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var yorumParametresi = context.ActionArguments.Values.OfType<YorumModel>().FirstOrDefault();

            if (yorumParametresi != null)
            {
                var yasakliKelimeler = new List<string> {"aptal", "salak", "gerizekalı"};

                foreach (var yasakliKelime in yasakliKelimeler)
                {
                    if (yorumParametresi.YorumIcerigi.Contains(yasakliKelime))
                    {
                        yorumParametresi.YorumIcerigi = yorumParametresi.YorumIcerigi.Replace(yasakliKelime,"***");
                    }
                }
            }

            //Filtre işlemi tamamlandıktan sonra base sınıfın metodunu çağırıyoruz.
            base.OnActionExecuting(context);

        }
    }
}
