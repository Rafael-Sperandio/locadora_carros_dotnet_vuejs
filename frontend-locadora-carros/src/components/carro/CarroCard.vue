<template>
  <div class="card " style="width: 22rem;">
    <img src="https://images.unsplash.com/photo-1583121274602-3e2820c69888?q=80&w=870&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" 
    class="card-img-top" 
    alt="...">
    <div class="card-header ">
      <div class="d-flex column justify-content-between align-items-center">
        <h5 class="card-text mb-0" style="font-size: 20px;">
          {{ carro.marca }} {{ carro.modelo }}
        </h5>
        <h5 class="card-text gap-1 d-flex">
          <span class="badge text-bg-primary d-flex align-items-center">{{ carro.categoria }} </span>
          <span class="badge text-bg-primary d-flex align-items-center">{{ carro.ano }}</span>
        </h5> 
      </div>

    </div>
    <div class="card-body ">

      
      <h5 class="card-title" >Valor diário:</h5>
      <h5 class="card-text">
         R$ {{ carro.valorDiaria }} /dia
      </h5>
      <router-link
        :to="`/carros/${carro.id}`"
        class="btn btn-primary w-100"
      >
        Detalhes carro
      </router-link>
    </div>

  </div>
</template>


<script lang="ts">
import type { Carro } from "../../types/Carro/Carro"
export default {
  name: 'CarroCard',

  props: {
    carro: {
      type: Object as () => Carro,
      required: true
    }
  }
}
</script>


<style scoped lang="scss">
.card-title{
  color: var(--color-gray);
  font-size: var(--font-size-sm);
}
.card-text{
  color: var(--color-text-primary);
  font-size: var(--font-size-md);
}
.card-body,.card-header{
    text-align: left;
    //background-color: var(--color-text-primary) ;

    background-color: var(--color-secondary) ;
    /* padding: 20px 20px 20px 0px; */
}


</style>