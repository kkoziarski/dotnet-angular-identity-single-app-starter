using CleanArchWeb.Application.Common.Interfaces;
using System;

namespace CleanArchWeb.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
