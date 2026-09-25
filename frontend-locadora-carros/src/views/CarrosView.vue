<template>
  <div class="container">

    <div class="d-flex justify-content-center align-items-center mb-4">

      <h1>Carros</h1>

      <!-- Componente: Botão Novo Carro -->

    </div>

    <!-- Componente: Filtro de Carros -->
    <!--
      Possíveis filtros:

      Marca
      Modelo
      Categoria
      Status
    -->
    <h2>lista carros</h2>
    <div class="d-flex flex-column justify-content-center align-items-center">
        <div class="mb-3"  v-for="(carro,index) in listaCarros" :key="index">

            <CarroDetalhe :carro="carro"></CarroDetalhe>
        </div>
      <!-- Componente: Tabela de Carros -->

      <!--
        Sugestão de colunas:

        Marca
        Modelo
        Ano
        Placa
        Categoria
        Valor da diária
        Status
        Ações
      -->

    </div>

    <!-- Componente: Paginação -->

  </div>
</template>

<script lang="ts">
import CarroDetalhe from '../components/carro/CarroDetalhe.vue'
import apiCarros from "../services/carroService"

import type { Carro } from '../types/Carro/Carro'


export default {
  components:{
    CarroDetalhe,
  },
  data() {
    return {
      listaCarros: [] as Carro[]
    }
  },

  created() {
    this.buscaCarros()
  },

  methods: {
    async buscaCarros(): Promise<void> {
      const carros = await apiCarros.getAll()

      this.listaCarros = carros
    }
  }
}
</script>