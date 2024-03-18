using Application.Core;
using Domain;
using MediatR;
using Microsoft.Extensions.Configuration;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ThucTheDinhDanh
{
    public class ThemMoi
    {
        public class Command : IRequest<Result<Domain.ThucTheDinhDanh>>
        {
            public Domain.ThucTheDinhDanh Entity { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<Domain.ThucTheDinhDanh>>
        {
            private readonly DataContext _context;
            private readonly IConfiguration _configuration;

            public Handler(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
            }
            public async Task<Result<Domain.ThucTheDinhDanh>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    await _context.ThucTheDinhDanh.AddAsync(request.Entity);
                    int rowInserted = await _context.SaveChangesAsync();
                    if (rowInserted <= 0)
                    {
                        throw new Exception("Thêm mới không thành công");
                    }
                    return Result<Domain.ThucTheDinhDanh>.Success(request.Entity);
                }
                catch(Exception ex)
                {
                    return Result<Domain.ThucTheDinhDanh>.Failure(ex.Message);
                }
            }
        }
    }
}
