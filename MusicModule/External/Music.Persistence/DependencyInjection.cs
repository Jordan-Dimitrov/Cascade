﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMusicPersistence(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            return services;
        }
    }
}
