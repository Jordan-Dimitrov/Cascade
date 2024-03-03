﻿using Domain.Shared.Abstractions;
using Music.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.RequestFeatures
{
    public class ArtistParameters : RequestParameters
    {
        public ArtistParameters()
        {
            OrderBy = "username";
        }
        public int MinAlbumCount { get; set; } = int.MinValue;
        public int MaxAlbumCount { get; set; } = int.MaxValue;
        public string? SearchTerm { get; set; }
    }
}