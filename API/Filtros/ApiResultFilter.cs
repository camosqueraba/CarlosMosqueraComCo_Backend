using DAL.DTOs.UtilDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filtros
{
    public class ApiResultFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (resultContext.Result is ObjectResult { Value: IApiResult apiResult })
            {
                var response = apiResult.ToResponse();
                resultContext.Result = new ObjectResult(response)
                {
                    StatusCode = response.StatusCode
                };
            }
        }
    }
}
