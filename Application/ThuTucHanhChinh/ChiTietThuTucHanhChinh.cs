using Application.Core;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Persistence;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ThuTucHanhChinh
{
    public class ChiTietThuTucHanhChinh
    {
        public class Query : IRequest<Result< Domain.ResponseEntities.ChiTietThuTucHanhChinh>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Domain.ResponseEntities.ChiTietThuTucHanhChinh>>
        {
            private readonly IConfiguration _configuration;
            private readonly DataContext _context;

            public Handler(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;

            }
            public async Task<Result<Domain.ResponseEntities.ChiTietThuTucHanhChinh>> Handle(Query request, CancellationToken cancellationToken)
            {
                using (var connettion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connettion.OpenAsync();
                    try
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@pId", request.Id);
                        Domain.ResponseEntities.ChiTietThuTucHanhChinh result = await connettion.QueryFirstOrDefaultAsync<Domain.ResponseEntities.ChiTietThuTucHanhChinh>("spu_ThuTucHanhChinh_ChiTiet", parameters, commandType: System.Data.CommandType.StoredProcedure);

                        if (result == null)
                        {
                            throw new Exception("Không tìm thấy dữ liệu");
                        }
                        return Result<Domain.ResponseEntities.ChiTietThuTucHanhChinh>.Success(result);
                    }
                    catch (Exception ex)
                    {
                        return Result<Domain.ResponseEntities.ChiTietThuTucHanhChinh>.Failure(ex.Message);
                    }
                    finally
                    {
                        await connettion.CloseAsync();
                    }
                }
            }
        }
    }
}
