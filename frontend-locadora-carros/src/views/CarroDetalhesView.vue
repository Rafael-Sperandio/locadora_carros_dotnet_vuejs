
<!-- TODO criar componente para carregamento -->
<template>
  <main class="container py-5">
    <div class="mb-3">
      <EtapasLocacao
        :classe-cabecalho="{ 'max-width': '187.5rem' }"
        :visivel="carro !== null"
        :carroId=" carro ? carro?.id : -1"
      />
    </div>

    <!-- Carregando -->
    <div
      v-if="carregando"
      class="text-center"
    >
      <div
        class="spinner-border"
        role="status"
      >
        <span class="visually-hidden">
          Carregando...
        </span>
      </div>

      <p class="mt-3">
        Carregando informações do carro...
      </p>
    </div>


    <!-- Erro -->
    <div
      v-else-if="erro"
      class="alert alert-danger"
      role="alert"
    >
      {{ erro }}

      <div class="mt-3">
        <button
          type="button"
          class="btn btn-secondary"
          @click="voltarParaCarros"
        >
          Voltar para carros
        </button>
      </div>
    <!-- Carro -->
    </div>
    <div
      v-else-if="carro"
    >
        <CarroDetalhe :carro="carro"> </CarroDetalhe>
    </div>

  </main>
</template>

<script lang="ts">
import carroService from '../services/carroService'
import type { Carro } from '../types/Carro/Carro'
import CarroDetalhe from '../components/carro/CarroDetalhe.vue'
import EtapasLocacao from '../components/locacao/EtapasLocacao.vue'

export default {
  name: 'CarroDetalheView',

  components: {
    CarroDetalhe,
    EtapasLocacao,
  },

  data() {
    return {
      carro: null as Carro | null,
      carregando: true,
      erro: ''
    }
  },

  async mounted() {
    try {
      const id = Number(this.$route.params.id)

      if (isNaN(id)) {
        this.erro = 'ID do carro inválido.'
        return
      }

      this.carro = await carroService.getById(id)
    } catch (error) {
      console.error(error)
      this.erro = 'Não foi possível carregar os dados do carro.'
    } finally {
      this.carregando = false
    }
  },

  methods: {
    voltarParaCarros() {
      this.$router.push('/carros')
    }
  }
}
</script>