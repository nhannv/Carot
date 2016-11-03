using System;
using AutoMapper;

namespace Carot.ERP.Api.Models.AutoMapper.Resolvers
{
    public class GuidToStringConverter : TypeConverter<Guid, string>
    {
        protected override string ConvertCore(Guid source)
        {
            return source.ToString("N");
        }
    }
}