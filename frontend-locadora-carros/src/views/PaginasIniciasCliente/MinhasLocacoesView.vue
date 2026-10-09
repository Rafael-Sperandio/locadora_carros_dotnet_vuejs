<template>
  <div class="container py-4">
    <div class="mb-4">
      <h2>Minhas Locações</h2>
      <p class="text-muted">
        Acompanhe o andamento de seus contratos, faturas, retiradas e devoluções.
      </p>
    </div>

    <!-- Filtros -->
    <div class="mb-4">
      <div class="btn-group" role="group">
        <button
          type="button"
          class="btn"
          :class="filtroAtual === 'TODAS' ? 'btn-success' : 'btn-outline-secondary'"
          @click="filtroAtual = 'TODAS'"
        >
          Todas
        </button>
        <button
          type="button"
          class="btn"
          :class="filtroAtual === 'ATIVA' ? 'btn-success' : 'btn-outline-secondary'"
          @click="filtroAtual = 'ATIVA'"
        >
          Ativas
        </button>
        <button
          type="button"
          class="btn"
          :class="filtroAtual === 'FINALIZADA' ? 'btn-success' : 'btn-outline-secondary'"
          @click="filtroAtual = 'FINALIZADA'"
        >
          Finalizadas
        </button>
        <button
          type="button"
          class="btn"
          :class="filtroAtual === 'CANCELADA' ? 'btn-success' : 'btn-outline-secondary'"
          @click="filtroAtual = 'CANCELADA'"
        >
          Canceladas
        </button>
      </div>
    </div>

    <!-- Tabela -->
    <div class="table-responsive">
      <table class="table table-hover align-middle">
        <thead>
          <tr>
            <th>CONTRATO</th>
            <th>VEÍCULO</th>
            <th>RETIRADA</th>
            <th>DEVOLUÇÃO</th>
            <th>VALOR TOTAL</th>
            <th>STATUS</th>
            <th>AÇÕES</th>
          </tr>
        </thead>

        <tbody>
          <tr
            v-for="locacao in locacoesFiltradas"
            :key="locacao.id"
          >
            <td>
              #{{ locacao.id }}
            </td>
            <td>
              <strong>
                {{ locacao.carro?.marca }}
                {{ locacao.carro?.modelo }}
              </strong>
              <div class="text-muted small">
                {{ locacao.carro?.categoria }}
                {{ locacao.carro?.ano }}
              </div>
            </td>
            <td>
              {{ formatarData(locacao.dataInicio) }}
            </td>
            <td>
              {{ formatarData(locacao.dataDevolucao ?? locacao.dataFim) }}
            </td>
            <td>
              {{ formatarMoeda(locacao.valorTotal) }}
            </td>
            <td>
              <span
                class="badge"
                :class="classeStatus(locacao.status)"
              >
                {{ locacao.status }}
              </span>
            </td>
            <td>
              <button
                type="button"
                class="btn btn-sm btn-outline-success"
                @click="verDetalhes(locacao.id)"
              >
                Ver Detalhes
              </button>
            </td>
          </tr>
          <tr v-if="locacoesFiltradas.length === 0">
            <td colspan="7" class="text-center py-4">
              Nenhuma locação encontrada.
            </td>
          </tr>
        </tbody>
      </table>
    </div>

  </div>
</template>

<script lang="ts">
import locacaoService from '../../services/locacaoService'
import type { ResponseLocacao } from '../../types/locacao/ResponseLocacao'
export default {
  name: 'MinhasLocacoesView',
  data() {
    return {
      locacoes: [] as ResponseLocacao[],
      filtroAtual: 'TODAS',
      carregando: false,
      erro: ''
    }
  },
  computed: {
    locacoesFiltradas(): ResponseLocacao[] {
      if (this.filtroAtual === 'TODAS') {
        return this.locacoes
      }
      return this.locacoes.filter(
        locacao => locacao.status === this.filtroAtual
      )
    }
  },

  async mounted() {
    await this.carregarLocacoes()
  },

  methods: {
    async carregarLocacoes() {
      this.carregando = true
      this.erro = ''
      try {
        const clienteId = 1
        this.locacoes =
          await locacaoService.getByClienteId(clienteId, true, false)
      } catch (error) {
        this.erro =
          'Não foi possível carregar suas locações.'
      } finally {
        this.carregando = false
      }
    },
    formatarData(data: string): string {
      if (!data) return ''
      const dataFormatada = new Date(data)
      return new Intl.DateTimeFormat('pt-BR', {
        day: '2-digit',
        month: 'short',
        year: 'numeric'
      }).format(dataFormatada)
    },
    formatarMoeda(valor: number): string {
      return new Intl.NumberFormat('pt-BR', {
        style: 'currency',
        currency: 'BRL'
      }).format(valor)
    },
    classeStatus(status: string): string {
      switch (status) {
        case 'ATIVA':
          return 'text-bg-success'
        case 'FINALIZADA':
          return 'text-bg-secondary'
        case 'CANCELADA':
          return 'text-bg-danger'
        default:
          return 'text-bg-secondary'
      }
    },
    verDetalhes(id: number) {
      this.$router.push(`/locacoes/${id}`)
    }
  }
}
</script>