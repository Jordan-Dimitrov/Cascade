﻿using Shared.Exceptions;
using Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Users.Domain.ValueObjects;

namespace Users.Domain.DomainEntities
{
    public sealed class RefreshToken : Entity
    {
        private Token _Token;
        private TokenDates _TokenDates;
        public RefreshToken(Token token, TokenDates tokenDates)
        {
            Id = Guid.NewGuid();
            Token = token;
            TokenDates = tokenDates;
        }

        [JsonConstructor]
        private RefreshToken()
        {

        }

        public Token Token
        {
            get
            {
                return _Token;
            }
            private set
            {
                _Token = value ?? throw new DomainValidationException("The name cannot be null");
            }
        }
        public TokenDates TokenDates
        {
            get
            {
                return _TokenDates;
            }
            private set
            {
                _TokenDates = value ?? throw new DomainValidationException("The dates cannot be null");
            }
        }
    }
}