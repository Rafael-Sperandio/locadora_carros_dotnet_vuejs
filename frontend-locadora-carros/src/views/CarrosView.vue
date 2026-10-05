<template>
  <div class="container">

    <div class="d-flex justify-content-center align-items-center mb-4">

      <h1>Carros disponiveis</h1>

    </div>
    <div class="d-flex gap-4 w-80 flitro">
        <input type="text" class="form-control">

        <select class="form-select">
            <option>Marca</option>
            <option>Modelo</option>
        </select>

        <select class="form-select">
            <option>Categoria</option>
            <option>SUV</option>
        </select>

        <button class="btn btn-primary">
            Buscar
        </button>
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

            <CarroCard :carro="carro"></CarroCard>
        </div>

    </div>

    <!-- Componente: Paginação -->

  </div>
</template>


<script setup lang="ts">
import { onMounted, ref } from 'vue'

import CarroCard from '../components/carro/CarroCard.vue'
import carroService from '../services/carroService'

import type { Carro } from '../types/Carro/Carro'

const listaCarros = ref<Carro[]>([])

async function buscaCarros(): Promise<void> {
  try {
    const carros = await carroService.getAll()

    listaCarros.value = carros
  } catch (error) {
    console.error('Erro ao buscar carros:', error)
  }
}

onMounted(() => {
  buscaCarros()
})
</script>



<style scoped lang="scss">
.flitro{
  background-color: var(--color-navbar);
  padding: var(--spacing-sm);
  border:  var(--radius-xs) solid var(--color-primary);
  //var(--radius-xs)
  border-radius: var(--radius-sm);
}
.adicionar-bnt{
  background-color: var(--color-navbar) ;

}
.adicionar-bnt:hover{
  background-color: var(--color-navbar);
}
</style>