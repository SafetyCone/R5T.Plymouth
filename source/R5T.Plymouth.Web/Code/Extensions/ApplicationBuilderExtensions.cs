using System;


namespace R5T.Plymouth.Web
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
