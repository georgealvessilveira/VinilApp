using System;
using Dommel;

namespace VinilApp.Test.Database.Config
{
    public class CustomTableNameResolver : DommelMapper.ITableNameResolver
    {
        public string ResolveTableName(Type type)
        {
            return $"{type.Name}";
        }
    }
}