using Application.Dtos;
using AutoMapper;
using Dapper;
using Domain.Aggregates.UserAggregate;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries
{
    internal sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IDbConnection _DbConnection;
        private readonly IMapper _Mapper;
        public GetUserByIdQueryHandler(IDbConnection dbConnection, IMapper mapper)
        {
            _DbConnection = dbConnection;
            _Mapper = mapper;
        }
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            const string sql = @"SELECT * FROM dbo.[User] WHERE Id = @UserId";

            UserDto user = await _DbConnection
                .QueryFirstOrDefaultAsync<UserDto>(
                sql,
                new {request.UserId});

            if(user is null)
            {
                throw new EntityNotFoundException(typeof(User));
            }

            return user;

        }
    }
}
