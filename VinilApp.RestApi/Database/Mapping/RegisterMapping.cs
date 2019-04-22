using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;

namespace VinilApp.RestApi.Database.Mapping
{
    public class RegisterMapping
    {
        public static void Register()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new DiscoMap());
                config.AddMap(new VendaMap());
                config.ForDommel();
            });
        }
    }
}