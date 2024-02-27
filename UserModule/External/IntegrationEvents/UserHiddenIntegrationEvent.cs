using Application.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.IntegrationEvents
{
    public sealed record UserHiddenIntegrationEvent(Guid UserId, int Role) : IntegragtionEvent;
}
