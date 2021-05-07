using System;

using R5T.Plymouth.Web;


namespace R5T.Plymouth
{
    public static class ApplicationBuilderExtensions
    {
        public static WebApplicationSpecification NewWebApplication(this ApplicationBuilder applicationBuilder)
        {
            var webApplicationSpecification = new WebApplicationSpecification();
            return webApplicationSpecification;
        }
    }
}
