using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace ManufactureWebApi_Core.Filters
{
    /// <summary>
    /// Фильтр, обрабатывающий все необработанные исключения
    /// </summary>
    public class ManufactureExceptionFilter : IExceptionFilter
    {
        //Среда времени выполнения
        private readonly bool _isDevelopment;

        public ManufactureExceptionFilter(IWebHostEnvironment env)
        {
            _isDevelopment = env.IsDevelopment();
        }

        /// <summary>
        /// Запускается при необработанном исключении
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            IActionResult actionResult;
            var ex = context.Exception;

            string? stackTrace = (_isDevelopment) ? ex.StackTrace : string.Empty;
            string message = ex.Message;

            if (ex is DbUpdateConcurrencyException)
            {
                if (!_isDevelopment)
                    message = "При обновлении базы данных возникла ошибка." +
                        " Запись была изменена другим пользователем";

                //Создание объекта BadRequestObjectResult на исключение DbUpdateConcurrencyException
                actionResult = new BadRequestObjectResult(
                    new { Error = "Проблема параллелизма", Message = message, StackTrace = stackTrace });
            }
            else
            {
                if (!_isDevelopment)
                    message = "Возникла неизвестная ошибка. Повторите действие.";

                //Создание объекта ObjectResult на любое исключение, кроме паралллизма
                actionResult = new ObjectResult(
                    new { Error = "Ошибка общего характера", Message = message, StackTrace = stackTrace })
                { 
                    StatusCode = 500
                };
            }

            context.Result = actionResult;
        }
    }
}
