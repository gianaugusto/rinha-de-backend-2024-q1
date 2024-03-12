using System.ComponentModel.DataAnnotations;

namespace rinha_2024_q1.Models
{
    public record TransacaoRequest(int Valor, [Required, StringLength(1)] string Tipo, [Required, StringLength(10)] string Descricao)
    {
        public (bool Valid, string Message) IsValidateModel()
        {
            if (!Constants.TiposValidos.Contains(Tipo))
            {
                return (false, "Transação inválida, tipo não disponivel. ");
            }

            if (Descricao.Length >  10)
            {
                return (false, "Transação inválida, descricao muito longa. ");
            }

            if (Valor < 1)
            {
                return (false, "Transação inválida, valor deve ser positivo. ");
            }

            return (true, string.Empty);
        }
    }
}
