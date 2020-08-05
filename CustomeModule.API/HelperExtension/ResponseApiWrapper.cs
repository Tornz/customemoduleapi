using CustomeModule.Interfaces.HelperExtensions.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomeModule.API.HelperExtension
{
    public static class ResponseApiWrapper
    {
        public static IActionResult ToHttpResponse<T>(this IListModelResponse<T> list)
        {
            return new ObjectResult(list);
        }

        public static IActionResult ToHttpResponse<T>(this ISingleModelResponse<T> single)
        {
            return new ObjectResult(single);
        }
    }
}
