namespace LocadoraCarrosBackEnd.Utils.constants
{
    public static class LocacaoErrors
    {
        public const string PeriodoInvalido = "A data de início deve ser anterior à data de fim.";

        public const string CarroIndisponivel ="Já existe uma locação para este carro nesse período.";

        public const string CarroNaoEncontrado = "Carro não encontrado.";

        public const string ClienteNaoEncontrado = "Cliente não encontrado.";

        public const string MudancaStatusIvalida = "Não é possível realizar essa alteração de status.";
    }
}
