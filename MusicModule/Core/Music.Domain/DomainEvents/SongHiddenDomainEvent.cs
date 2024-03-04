﻿using Domain.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.DomainEvents
{
    public sealed record SongHiddenDomainEvent(string FileName) : IDomainEvent;
}