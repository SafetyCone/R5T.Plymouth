using System;
using System.Threading.Tasks;


namespace R5T.Plymouth
{
    public interface IApplicationBuilder<TApplication>
    {
        Task<TApplication> Build(IApplicationSpecification applicationSpecification);
    }
}
