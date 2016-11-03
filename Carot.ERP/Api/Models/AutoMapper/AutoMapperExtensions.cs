﻿using System;
using AutoMapper;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Carot.ERP.Api.Models.AutoMapper
{
    public static class AutoMapperExtensions
    {
        public static List<TResult> MapTo<TResult>(this IEnumerable self)
        {
            if (self == null)
                throw new ArgumentNullException();

            return (List<TResult>)Mapper.Map(self, self.GetType(), typeof(List<TResult>));
        }

        public static TResult MapTo<TResult>(this object self)
        {
            if (self == null)
                throw new ArgumentNullException();

            return (TResult)Mapper.Map(self, self.GetType(), typeof(TResult));
        }

        public static TResult MapPropertiesToInstance<TResult>(this object self, TResult value)
        {
            if (self == null)
                throw new ArgumentNullException();

            return (TResult)Mapper.Map(self, value, self.GetType(), typeof(TResult));
        }

        public static TResult DynamicMapTo<TResult>(this object self)
        {
            if (self == null)
                throw new ArgumentNullException();

            return (TResult)Mapper.DynamicMap(self, self.GetType(), typeof(TResult));
        }

		public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
		{
			var sourceType = typeof(TSource);
			var destinationType = typeof(TDestination);
			var existingMaps = Mapper.GetAllTypeMaps().First(x => x.SourceType.Equals(sourceType)
				&& x.DestinationType.Equals(destinationType));
			foreach (var property in existingMaps.GetUnmappedPropertyNames())
			{
				expression.ForMember(property, opt => opt.Ignore());
			}
			return expression;
		}
	}
}