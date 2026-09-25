import api from './api'
import type { Carro } from '../types/Carro/Carro.ts'

export default {
  async getAll(): Promise<Carro[]> {
    const response = await api.get<Carro[]>('/Carro')

    return response.data
  },

  async getById(id: number): Promise<Carro> {
    const response = await api.get<Carro>(`/Carro/${id}`)

    return response.data
  },

  async create(carro: Carro): Promise<Carro> {
    const response = await api.post<Carro>('/Carro', carro)

    return response.data
  },

  async update(carro: Carro): Promise<Carro> {
    const response = await api.put<Carro>('/Carro', carro)

    return response.data
  },

  async delete(id: number): Promise<void> {
    await api.delete(`/Carro/${id}`)
  }
}