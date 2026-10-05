
<template>
    <div :class="classeCabecalho" v-if="visivel">
      <div class="row text-center">
        <ul class="nav nav-underline nav-fill">
          <li class="nav-item">
            <span
              class="nav-link"
              :class="{ active: rotaAtiva(`/carros/${this.carroId}`) }"
            >
              1. Selecionar Carro
            </span>
          </li>

          <li class="nav-item">
            <span
              class="nav-link"
              :class="{ active: rotaAtiva(`/carros/${this.carroId}/locacao`) }"
            >
              2. Período da Locação
            </span>
          </li>

          <li class="nav-item">
            <span
              class="nav-link"
              :class="{ active: rotaAtiva('/confirmacao-locacao') }"
            >
              3. Confirmação
            </span>
          </li>
        </ul>
      </div>
    </div>
</template>

<script lang="ts">
export default {
  name: 'EtapasLocacao',

  props: {

    classeCabecalho: {
      type: String,
      default: ''
    },

    visivel: {
      type: Boolean,
      default: false
    },
    carroId: {
      type: Number,
      default: -1
    }
  },
  methods: {
    rotaAtiva(rota: string): boolean {
      console.log('rotaAtiva:', this.$route.path, 'rota:', rota);
      return this.$route.path === rota;
    }
  }
}
</script>

