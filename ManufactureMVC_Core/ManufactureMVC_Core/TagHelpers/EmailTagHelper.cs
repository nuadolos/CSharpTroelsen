using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ManufactureMVC_Core.TagHelpers
{
    /// <summary>
    /// Специальная вспомогательная функция дескрипторов
    /// для указания email
    /// </summary>
    public class EmailTagHelper : TagHelper
    {

        /* 
         * Чтобы иметь возможность использовать данный дескриптор необходимо:
         *      1) Обозначить уровень доступа к дескриптору
         *          - Для глобального доступа: Views => _ViewImports
         *          - Для представлений одного контроллера: Views => Контроллер => _ViewImports
         *          - Для одного представления: Views => Контроллер => Представление
         *          
         *      2) Прописать сл. строку кода: 
         *          @addTagHelper *, ManufactureMVC_Core
         * 
         * Внешний вид вспомогательной функции дескриптора:
         *      <email email-name="2nuadolos1" email-domain="gmail.com"></email>*/

        /// <summary>
        /// Отображается в дескрипторе как email-name
        /// </summary>
        public string EmailName { get; set; }
        /// <summary>
        /// Отображается в дескрипторе как email-domain
        /// </summary>
        public string EmailDomain { get; set; }

        /// <summary>
        /// Используется при обращение к функции дескриптора <email>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //Замена <email> дескриптором <a>
            output.TagName = "a";
            
            var address = $"{EmailName}@{EmailDomain}";

            //Составление ссылки на почту
            //(Т.е. переадресация на почту пользователя с окном отправления письма с указанным отправителем)
            output.Attributes.SetAttribute("href", $"mailto: {address}");

            //Отображение почты
            output.Content.SetContent(address);
        }
    }
}
