import api from './api'

import type { ResponseLocacao } from '../types/locacao/ResponseLocacao'

export default {
  async getByClienteId(id: number, includeCarro: boolean =false, includeCliente: boolean =false): Promise<ResponseLocacao[]> {
    const response = await api.get<ResponseLocacao[]>(
      `/locacao/cliente/${id}?includeCarro=${includeCarro}&includeCliente=${includeCliente}`
    )

    return response.data.map(locacao => ({
      ...locacao,
      dataInicio: new Date(locacao.dataInicio),
      dataFim: new Date(locacao.dataFim),
      dataDevolucao: locacao.dataDevolucao
        ? new Date(locacao.dataDevolucao)
        : null
    }))
  },
async getById(id: number, includeCarro: boolean =false, includeCliente: boolean =false): Promise<ResponseLocacao> {
  const response = await api.get(`/locacao/${id}?includeCarro=${includeCarro}&includeCliente=${includeCliente}`)

  const locacao = response.data

  return {
    ...locacao,
    dataInicio: new Date(locacao.dataInicio),
    dataFim: new Date(locacao.dataFim),
    dataDevolucao: locacao.dataDevolucao
      ? new Date(locacao.dataDevolucao)
      : null
  }
}

}