using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InputWebApp.Controllers
{
    public class BaseViewController : Controller
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
