using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Dapper;
using Persistence;
using Application.Core;
using Domain;

namespace Application.NhomThuTucCon
{
    public class CapNhat
    {
        public class Command : IRequest<Result<SubNhomThuTuc>>
        {
            public int Id { get; set; }
            public SubNhomThuTuc Entity { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<SubNhomThuTuc>>
        {
            private readonly DataContext _context;
            private readonly IConfiguration _configuration;

            public Handler(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
            }

            public async Task<Result<SubNhomThuTuc>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var entity = await _context.SubNhomThuTuc.FindAsync(request.Id);
                    if (entity == null)
                    {
                        throw new Exception("Không tìm thấy đối tượng");
                    }
                    entity.MoTa = request.Entity.MoTa;
                    entity.ParentId = request.Entity.ParentId;
                    entity.TenNhom = request.Entity.TenNhom;
                    var updateResult = await _context.SaveChangesAsync();
                    if (updateResult <= 0)
                    {
                        throw new Exception("Cap nhat that bai");
                    }

                    return Result<SubNhomThuTuc>.Success(entity);
                }
                catch (Exception ex)
                {
                    return Result<SubNhomThuTuc>.Failure(ex.Message);
                }
            }
        }
    }
}
