import { Carro } from "../Carro/Carro";

export type ResponseLocacao = {
    id: number;
    clienteId: number;
    carroId: number;
    dataInicio: Date;
    dataFim: Date;
    dataDevolucao: Date | null;
    valorDiaria: number;
    valorTotal: number;
    status: string;
    carro: Carro | null;
};