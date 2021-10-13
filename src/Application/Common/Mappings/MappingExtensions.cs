using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchWeb.Application.Common.Models;
using CleanArchWeb.Domain.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CleanArchWeb.Application.Common.Mappings
{
    public static class MappingExtensions
    {

        [Obsolete("do not use it - its working in memory instead of database")]
        public static PaginatedList<TDestination> ToPaginatedList<TDestination>(this IQueryable<TDestination> enumerable, int pageNumber, int pageSize)
            => PaginatedList<TDestination>.Create(enumerable, pageNumber, pageSize);

        public static List<TDestination> ProjectToList<TDestination>(this IQueryable queryable, IConfigurationProvider configuration)
            => queryable.ProjectTo<TDestination>(configuration).ToList();

        public static IMongoQueryable<TDestination> ProjectTo<TSource, TDestination>(this IMongoQueryable<TSource> query, IConfigurationProvider configuration) =>
                query.ProjectTo<TDestination>(configuration) as IMongoQueryable<TDestination>;

        public static ProjectionDefinition<TodoListDocument, TodoLists.Queries.GetTodos.TodoListDto> TestProjection(IMapper mapper)
        {
            var projection = Builders<TodoListDocument>.Projection.Expression(x => mapper.Map<TodoLists.Queries.GetTodos.TodoListDto>(x));
            return projection;
        }
    }
}
