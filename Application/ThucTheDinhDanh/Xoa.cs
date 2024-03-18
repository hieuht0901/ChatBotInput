using Application.Core;
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
    public class Xoa
    {
        public class Command : IRequest<Result<int>>
        {
            public Guid Id { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<int>>
        {
            private readonly DataContext _context;
            private readonly IConfiguration _configuration;

            public Handler(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
            }

            public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var thucthe = await _context.ThucTheDinhDanh.FindAsync(request.Id);
                    if (thucthe == null)
                    {
                        throw new Exception("Không tìm thấy dữ liệu");
                    }
                    _context.ThucTheDinhDanh.Remove(thucthe);
                    int rowUpdated = await _context.SaveChangesAsync();
                    if (rowUpdated <= 0)
                    {
                        throw new Exception("Xoa không thành công");
                    }
                    return Result<int>.Success(rowUpdated);
                }
                catch (Exception ex)
                {
                    return Result<int>.Failure(ex.Message);
                }
            }
        }
    }
}
