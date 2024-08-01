using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPIAlmacen.Filters
{
    public class FiltroDeExcepcion : ExceptionFilterAttribute
    {
        private readonly ILogger<FiltroDeExcepcion> logger;
        private readonly IWebHostEnvironment env;

        public FiltroDeExcepcion(ILogger<FiltroDeExcepcion> logger, IWebHostEnvironment env)
        {
            this.logger = logger;
            this.env = env;
        }

        public override void OnException(ExceptionContext context)
        {
            logger.LogError(context.Exception, context.Exception.Message);

            var path = $@"{env.ContentRootPath}\wwwroot\log.txt";
            using (StreamWriter writer = new StreamWriter(path, append: true))
            {
                writer.WriteLine(context.Exception.Message);
            }

            base.OnException(context);
        }
    }

}
