﻿using Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Aggregates.UserAggregate;

namespace Users.Domain.DomainEvents
{
    public sealed record UserCreatedDomainEvent(User user) : IDomainEvent;
}