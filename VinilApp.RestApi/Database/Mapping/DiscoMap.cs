using Dapper.FluentMap.Dommel.Mapping;
using VinilApp.RestApi.Model;

namespace VinilApp.RestApi.Database.Mapping
{
    public class DiscoMap : DommelEntityMap<Disco>
    {
        public DiscoMap()
        {
            ToTable("disco");
            Map(d => d.Id).ToColumn("id").IsKey().IsIdentity();
            Map(d => d.Nome).ToColumn("nome");
            Map(d => d.Genero).ToColumn("genero");
            Map(d => d.Preco).ToColumn("preco");
        }
    }
}