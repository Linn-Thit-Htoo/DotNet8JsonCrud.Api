﻿using DotNet8JsonCrud.Api.Features.Blog;
using DotNet8JsonCrud.Api.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace DotNet8JsonCrud.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFeatures(this IServiceCollection services)
        {
            return services.AddScoped<JsonFileHelper>().AddScoped<BL_Blog>();
        }
    }
}