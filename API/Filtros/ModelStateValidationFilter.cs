using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using DAL.DTOs.UtilDTOs;

namespace API.Filtros
{
    public class ModelStateValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var response = new ApiResponse<string>(false, 400, "error en validaciones de campos", "", errors);
                context.Result = new BadRequestObjectResult(response);
            }
        }
    }
}