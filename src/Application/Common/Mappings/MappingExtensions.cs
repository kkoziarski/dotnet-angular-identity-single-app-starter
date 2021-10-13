using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchWeb.Application.Common.Models;
using CleanArchWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CleanArchWeb.Application.Common.Mappings
{
    public static class MappingExtensions
    {
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
            => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);

        [Obsolete("do not use it - its working in memory instead of database")]
        public static PaginatedList<TDestination> ToPaginatedList<TDestination>(this IEnumerable<TDestination> enumerable, int pageNumber, int pageSize)
            => PaginatedList<TDestination>.Create(enumerable, pageNumber, pageSize);

        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration)
            => queryable.ProjectTo<TDestination>(configuration).ToListAsync();

        public static IMongoQueryable<TDestination> ProjectTo<TSource, TDestination>(this IMongoQueryable<TSource> query, IConfigurationProvider configuration) =>
                query.ProjectTo<TDestination>(configuration) as IMongoQueryable<TDestination>;

        public static ProjectionDefinition<TodoListDocument, TodoLists.Queries.GetTodos.TodoListDto> TestProjection(IMapper mapper)
        {
            var projection = Builders<TodoListDocument>.Projection.Expression(x => mapper.Map<TodoLists.Queries.GetTodos.TodoListDto>(x));
            return projection;
        }
    }
}
