/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using ADX.DataAccessLayer.Repositories.Interfaces;
using ADX.Entities.DomainModel;

namespace ADX.DataAccessLayer.Repositories
{
    public class SystemParameterRepository : GenericDataRepository<SystemParameter>, ISystemParameterRepository
    {
    }
}