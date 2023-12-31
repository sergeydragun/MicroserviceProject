using AutoMapper;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using PlatformService.Data.Intrefaces;

namespace PlatformService.BLL.SyncDataServices.Grpc
{
    public class GrpcPlatformService : GrpcPlatform.GrpcPlatformBase
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public GrpcPlatformService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _database = unitOfWork;
            _mapper = mapper;
        }

        public override async Task<PlatformResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context)
        {
            var response = new PlatformResponse();

            var platforms = await _database.Platforms.GetAll().ToListAsync();

            foreach(var plat in platforms)
            {
                response.Platform.Add(_mapper.Map<GrpcPlatformModel>(plat));
            }

            return response;
        }
    }
}
