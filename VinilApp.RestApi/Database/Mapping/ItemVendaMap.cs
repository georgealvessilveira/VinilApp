using Dapper.FluentMap.Dommel.Mapping;
using VinilApp.RestApi.Model;

namespace VinilApp.RestApi.Database.Mapping
{
    public class ItemVendaMap : DommelEntityMap<ItemVenda>
    {
        public ItemVendaMap()
        {
            ToTable("item_venda");
            Map(iv => iv.Id).ToColumn("id").IsKey().IsIdentity();
            Map(iv => iv.DiscoId).ToColumn("discoId");
            Map(iv => iv.VendaId).ToColumn("vendaId");
            Map(iv => iv.Valor).ToColumn("valor");
        }
    }
}