<template>
  <div class="container py-4 ">
    <!-- Cabeçalho -->
    <div class="d-flex justify-content-between align-items-center mb-1">
      <div>
        <h2 class="mx-2">
          Detalhes da Locação <span v-if="locacao">#{{ locacao?.id }}</span>
        </h2>
      </div>
      <span
        v-if="locacao"
        class="badge fs-6"
        :class="classeStatus(locacao.status)"
      >
        {{ locacao.status }}
      </span>
    </div>
    <!-- Erro -->
    <div
      v-if="erro"
      class="alert alert-danger"
    >
      {{ erro }}
    </div>
    <!-- Carregando -->
    <div
      v-if="carregando"
      class="text-center py-5"
    >
      <div
        class="spinner-border"
        role="status"
      >
        <span class="visually-hidden">
          Carregando...
        </span>
      </div>
    </div>
    <!-- Conteúdo -->
    <div v-if="locacao && !carregando" class="d-flex flex-row">
      <!-- Veículo -->
      <div class="col-md-6"
        v-if="locacao.carro">
        <CarroSelecionado
          :carro="locacao?.carro"
          :titulo="'sobre o veículo'"
        />
      </div>
      <div
        v-else
        class="text-muted col-md-6"
      >
        Informações do veículo não disponíveis.
      </div>
      <!-- Valores -->
      <div class="col-md-6">
        <div class="card m-2" v-if="locacao">
          <div class="card-header align-items-start d-flex ">
            <h5 class="mb-0">
              Resumo do contrato
            </h5>
          </div>
          <div class="card-body">
            <div class="d-flex justify-content-between mb-2">
              <span>
                periodo de locação
              </span>
              <span>
                {{ quantidadeDiasLocacao }} 
                {{ quantidadeDiasLocacao === 1 || quantidadeDiasLocacao === 0 ? ' dia' : ' dias' }}
                <span>
                {{ '(' + formatarData(locacao.dataInicio) +' a ' + formatarData(locacao.dataFim) + ')' }}
                </span>
              </span>
            </div>
            <div class="d-flex justify-content-between mb-2">
              <span>
                Valor da diária
              </span>
              <span>
                {{ formatarMoeda(this.locacao?.carro ? this.locacao.carro.valorDiaria : 0) }}
              </span>
            </div>
            <div class="d-flex justify-content-between mb-2">
              <span>
                Tipo de seguro
              </span>
              <span>
                {{ locacao?.carro?.tipoSeguro || 'Não informado' }}
              </span>
            </div>
            <hr>
            <div class="d-flex justify-content-between">
              <strong>
                Valor total
              </strong>
              <strong class="fs-5">
                {{ formatarMoeda(valorTotalLocacao) }}
              </strong>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- Ações -->
    <div class="d-flex justify-content-between">
      <button
        type="button"
        class="btn btn-outline-secondary"
        @click="$router.push('/minhas-locacoes')"
      >
        Voltar para Minhas Locações
      </button>
    </div>
  </div>
</template>
<script lang="ts">
import locacaoService from '../../services/locacaoService'
import CarroSelecionado from '../../components/carro/CarroSelecionado.vue'
import type { ResponseLocacao } from '../../types/locacao/ResponseLocacao'
import {
  formatarData,
  formatarMoeda,
  quantidadeDias,
  valorTotal
} from '../../utils/locacaoUtils'; 
export default {
  name: 'DetalhesLocacaoView',
  data() {
    return {
      locacao: null as ResponseLocacao | null,
      carregando: false,
      erro: ''
    }
  },
  components: {
    CarroSelecionado
  },
  computed: {
    quantidadeDiasLocacao(): number {
      if (!this.locacao) return 0;
      if (!this.locacao.dataInicio ) return 0;
      if (!this.locacao.dataFim) return 0;
      return quantidadeDias(
        this.locacao.dataInicio,
        this.locacao.dataFim
      );
    },
    valorTotalLocacao(): number {
      if (!this.locacao) return 0;
      if (!this.locacao.carro) return 0;
      return valorTotal(
        this.quantidadeDiasLocacao,
        this.locacao.carro.valorDiaria
      );
    }
  },
  async mounted() {
    await this.carregarLocacao()
  },
  methods: {
    async carregarLocacao() {
      this.carregando = true
      this.erro = ''
      try {
        const id = Number(this.$route.params.id)
        if (Number.isNaN(id)) {
          this.erro = 'Identificador da locação inválido.'
          return
        }
        this.locacao =
          await locacaoService.getById(id, true, false)
      } catch (error) {
        this.erro =
          'Não foi possível carregar os detalhes da locação.'
      } finally {
        this.carregando = false
      }
    },
    formatarData,
    formatarMoeda,
    formatarDataDevolucao(
      data: string | null
    ): string {
      if (!data) {
        return 'Ainda não informada'
      }
      return this.formatarData(data);
    },
    classeStatus(status: string): string {
      switch (status) {
        case 'Ativa':
          return 'text-bg-success'
        case 'Finalizada':
          return 'text-bg-secondary'
        case 'Cancelada':
          return 'text-bg-danger'
        case 'Reservada':
          return 'text-bg-warning'
        default:
          return 'text-bg-secondary'
      }
    }
  }
}
</script>