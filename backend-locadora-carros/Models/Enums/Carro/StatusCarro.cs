namespace LocadoraCarros.Models.Enums.Carro
{
    /// <summary>
    /// Representa a situação atual de um veículo na locadora.
    /// </summary>
    public enum StatusCarro
    {
        Disponivel,//padrão inicial
        Alugado, // não faz sentido durante qual periodo
        //nomenclatura não esclarece qual das duas situações meljor se encaixa
        //só se for veiculo que foi alugado pela propria locadora por outra
        // está alugando para outro locadora diretamente
        Manutencao, // está no concerto
        Inativo // vendido/deletado
    }
}
