using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Shop_Project.Filter
    {
    public class TimeFilter :Attribute, IResourceFilter
        {
        Stopwatch sw= new Stopwatch();
        




        public void OnResourceExecuting(ResourceExecutingContext context)
            {
            sw.Start();
            }

        public void OnResourceExecuted(ResourceExecutedContext context)
            {
            sw.Stop();

            string str = $"response - {sw.ElapsedMilliseconds} ms";
            context.HttpContext.Response.Headers.Add("time", str);
            }
        }
    }
