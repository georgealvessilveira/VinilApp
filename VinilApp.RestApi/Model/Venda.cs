using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using NodaTime;

namespace VinilApp.RestApi.Model
{
    public class Venda
    {
        [JsonProperty(PropertyName = "id")] public long Id { get; set; }
        [JsonProperty(PropertyName = "data")] public LocalDate Data { get; set; }
        [JsonProperty(PropertyName = "itens_venda")] public List<ItemVenda> ItensVenda { get; set; } = new List<ItemVenda>();
        [JsonProperty(PropertyName = "valor_total")] public double ValorTotal => ItensVenda.Select(itemVenda => itemVenda.Valor).Sum();
    }
}