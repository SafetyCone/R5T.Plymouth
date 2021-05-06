using System;


namespace R5T.Plymouth
{
    public static class ApplicationBuilderExtensions
    {
        public static ApplicationSpecification NewApplication(this ApplicationBuilder applicationBuilder)
        {
            var applicationSpecification = new ApplicationSpecification();
            return applicationSpecification;
        }
    }
}
