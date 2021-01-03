using LandonHotelAPI.Infrastructure;
using LandonHotelAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LandonHotelAPI.Filters
{
    public class LinkRewritingFilter : IAsyncResultFilter
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        public LinkRewritingFilter(IUrlHelperFactory urlHelper)
        {
            _urlHelperFactory = urlHelper;
        };

        public Task OnResultExecutionAsync
            (ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var asObjectResult = context.Result as ObjectResult;
            bool shouldSkip = asObjectResult?.StatusCode >= 400
                || asObjectResult?.Value == null
                || asObjectResult?.Value as Resource == null;

            if (shouldSkip)
            {
                return next();
            }

            var rewriter = new LinkRewriter(_urlHelperFactory.GetUrlHelper(context));
            RewriteAllLinks(asObjectResult.Value, rewriter);

            return next();
        }
        private static void RewriteAllLinks(object model, LinkRewriter rewriter)
        {
            if (model == null) return;

            var allProperties = model
                .GetType().GetTypeInfo()
                .GetAllProperties()
                .Where(p => p.CanRead)
                .ToArray();
         
   
        }
    }
}
