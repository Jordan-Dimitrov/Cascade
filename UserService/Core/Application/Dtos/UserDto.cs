using Application.Abstractions;
using Domain.Aggregates.UserAggregate;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public UserRole PermissionType { get; set; }
        public List<Link> Links { get; set; } = new List<Link>();
    }
}