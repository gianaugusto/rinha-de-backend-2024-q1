namespace rinha_2024_q1.Models
{
    public record ExtratoResult(SaldoResult Saldo, IEnumerable<Transacao> UltimasTransacaos);

    public record SaldoResult(int Total, DateTime DataExtrato, int Limite);

}
