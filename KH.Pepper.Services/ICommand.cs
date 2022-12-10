using MediatR;

namespace KH.Pepper.Core.AppServices
{
    //This is marker interface to be used in MediatR<TResponse>
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
