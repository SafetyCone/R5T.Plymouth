using System;
using System.Threading.Tasks;


namespace R5T.Plymouth.Web
{
    public interface IWebApplicationBuilder<TWebApplication>
    {
        Task<TWebApplication> Build(IWebApplicationSpecification webApplicationSpecification);
    }
}
