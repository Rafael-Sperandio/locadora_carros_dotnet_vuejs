<template>
  <div class="card " style="width: 20rem;">
    <div class="card-body">

      <h5 class="card-title">
        {{ carro.marca }} - {{ carro.modelo }}
      </h5>

      <h5 class="card-text">
        {{ carro.categoria }} - {{ carro.ano }} - {{ carro.placa }}
      </h5>

      <h3 class="card-text">
        Valor diário: {{ carro.valorDiaria }} R$
      </h3>

    </div>

    <ul class="list-group list-group-flush">
    </ul>

    <div class="card-body d-flex justify-content-between">
      <a href="#" class="btn btn-primary">
        Alugar
      </a>

      <a href="#" class="btn btn-primary">
        Detalhes
      </a>
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