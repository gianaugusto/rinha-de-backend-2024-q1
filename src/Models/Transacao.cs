using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace rinha_2024_q1.Models
{
    public record Transacao([Required] int ClientId, [Required, StringLength(10)] string Descricao, [Required, StringLength(1)] string Tipo, [Required] int Valor)
    {
        public int Id { get; set; }

        public DateTime RealizadaEm { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public virtual ClienteExtrato Extrato { get; set; }

    }
}