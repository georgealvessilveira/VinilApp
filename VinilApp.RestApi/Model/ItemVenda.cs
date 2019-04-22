using Newtonsoft.Json;

namespace VinilApp.RestApi.Model
{
    public class ItemVenda
    {
        [JsonProperty(PropertyName = "id")] public long Id { get; set; }
        [JsonProperty(PropertyName = "disco_id")] public long DiscoId { get; set; }
        [JsonProperty(PropertyName = "disco")] public Disco Disco { get; set; }
        [JsonProperty(PropertyName = "venda_id")] public long VendaId { get; set; }
        [JsonProperty(PropertyName = "valor")] public double Valor { get; set; }
        [JsonProperty(PropertyName = "cashback")] public double Cashback { get; set; }
    }
}