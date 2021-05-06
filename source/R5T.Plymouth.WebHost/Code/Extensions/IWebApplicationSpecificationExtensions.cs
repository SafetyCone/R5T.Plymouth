﻿using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;

using R5T.Plymouth.Web;


namespace R5T.Plymouth.WebHost
{
    public static class IWebApplicationSpecificationExtensions
    {
        public static Task<IWebHost> BuildWebHost(this IWebApplicationSpecification webApplicationSpecification, WebHostBuilder webHostBuilder)
        {
            return webHostBuilder.Build(webApplicationSpecification);
        }

        public static Task<IWebHost> BuildWebHost(this IWebApplicationSpecification webApplicationSpecification)
        {
            var webHostBuilder = new WebHostBuilder();

            return webApplicationSpecification.BuildWebHost(webHostBuilder);
        }
    }
}