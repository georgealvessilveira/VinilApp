using System;
using Dommel;

namespace VinilApp.RestApi.Database.Util
{
    public static class DatabaseUtils
    {
        public static string Table<TTable>()
        {
            var type = typeof(TTable);
            return DommelMapper.Resolvers.Table(type);
        }
    }
}