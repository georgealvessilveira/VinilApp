using Dapper.FluentMap.Dommel.Mapping;
using VinilApp.RestApi.Model;

namespace VinilApp.RestApi.Database.Mapping
{
    public class VendaMap : DommelEntityMap<Venda>
    {
        public VendaMap()
        {
            ToTable("venda");
            Map(v => v.Id).ToColumn("id").IsKey().IsIdentity();
            Map(v => v.Data).ToColumn("nome");
        }
    }
}