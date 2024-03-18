using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;

namespace Application.ThucTheDinhDanh
{
    public class DanhSach
    {
        public class Query : IRequest<Result<List<Domain.ThucTheDinhDanh>>> { }
        public class Handler : IRequestHandler<Query, Result<List<Domain.ThucTheDinhDanh>>>
        {
            private readonly DataContext _context;
            private readonly IConfiguration _configuration;

            public Handler(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
            }
            public async Task<Result<List<Domain.ThucTheDinhDanh>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var ds = await _context.ThucTheDinhDanh.ToListAsync<Domain.ThucTheDinhDanh>();
                return Result<List<Domain.ThucTheDinhDanh>>.Success(ds);
            }
        }
    }
}
