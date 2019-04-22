using Newtonsoft.Json;

namespace VinilApp.RestApi.Model
{
    public class Disco
    {
        [JsonProperty(PropertyName = "id")] public long Id { get; set; }
        [JsonProperty(PropertyName = "nome")] public string Nome { get; set; }
        [JsonProperty(PropertyName = "genero")] public string Genero { get; set; }
        [JsonProperty(PropertyName = "preco")] public double Preco { get; set; }
    }
}