using Grpc.Core;
using GrpcServiceAlmacen.Models;
using GrpcServiceAlmacen.Protos;
using Microsoft.EntityFrameworkCore;

namespace GrpcServiceAlmacen.Services
{
    public class FamiliaGrpcService : FamiliaGrpc.FamiliaGrpcBase
    {
        // FamiliaGrpcBase es la superclase que el framework gRPC ha generado de forma automática.
        // Si hacemos Ctrl + click en FamiliaGrpcBase veremos el código fuente del código autogenerado

        private readonly MiAlmacenContext _context;

        public FamiliaGrpcService(MiAlmacenContext context)
        {
            _context = context;
        }

        public override async Task<FamiliasReply> GetFamilias(GetFamiliasRequest request, ServerCallContext context)
        {
            var response = new FamiliasReply
            {
                Familias = { }
            };
            var familias = await _context.Familias.ToListAsync();
            foreach (var familia in familias)
            {
                response.Familias.Add(new FamiliaReply
                {
                    Id = familia.Id,
                    Nombre = familia.Nombre
                });
            }

            return response;
        }

        public override async Task<FamiliaReply> AddFamilia(AddFamiliaRequest request, ServerCallContext context)
        {
            var newFamilia = new Familia
            {
                Nombre = request.Nombre
            };

            await _context.AddAsync(newFamilia);
            await _context.SaveChangesAsync();
            var response = new FamiliaReply
            {
                Id = newFamilia.Id,
                Nombre = newFamilia.Nombre
            };

            return response;
        }
    }

}
