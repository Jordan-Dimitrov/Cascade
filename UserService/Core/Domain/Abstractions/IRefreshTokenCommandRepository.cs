using Domain.Entities;
using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface IRefreshTokenCommandRepository : ICommandRepository<RefreshToken>
    {
    }
}
