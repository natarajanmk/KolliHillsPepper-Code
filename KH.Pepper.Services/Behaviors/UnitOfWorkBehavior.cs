using KH.Pepper.Core.Domain;
using MediatR;

namespace KH.Pepper.Core.AppServices
{
    public class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {

        private readonly IUnitOfWork _context;

        public UnitOfWorkBehavior(IUnitOfWork context)
        {
            _context = context;
        } 

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is ICommand<TResponse>)
            {
                var response = await next();

                await _context.SaveChangesAsync(cancellationToken);
                return response;
            }
            return await next();
        }
    }
}
