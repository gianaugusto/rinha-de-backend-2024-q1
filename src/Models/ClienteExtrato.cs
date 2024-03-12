namespace rinha_2024_q1.Models
{
    public class ClienteExtrato(int clienteId, int limite, int saldo)
    {
        public int ClienteId { get; set; } = clienteId;

        public int Limite { get; set; } = limite;

        public int Saldo { get; set; } = saldo;

        public virtual IList<Transacao> Transacaos { get; set; } = [];

        public virtual (bool Valid, string Message) NovaTransacao(Transacao transacao)
        {
                
            // add transação
            this.Transacaos.Add(transacao);

            if (transacao.Tipo.ToLowerInvariant().Equals("d"))
            {
                this.Saldo -= transacao.Valor;

                return ValidarSaldo();
            }

            this.Saldo += transacao.Valor;

            return ValidarSaldo();
        }

        private (bool valida, string mensagem) ValidarSaldo()
        {
            if (Saldo < (Limite * -1))
                return (false, "Transação inválida, você ultrapassou o limite disponivel.");

            return (true, string.Empty);
        }
    }
}
