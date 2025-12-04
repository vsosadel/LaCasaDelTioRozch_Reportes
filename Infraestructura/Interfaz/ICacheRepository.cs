using Infraestructura.DTO;

namespace Infraestructura.Interfaz
{
    public interface ICacheRepository
    {
        Task<CacheDTO> PorId(CacheDTO iCache);
        Task Registra(CacheDTO iCache);
    }
}
