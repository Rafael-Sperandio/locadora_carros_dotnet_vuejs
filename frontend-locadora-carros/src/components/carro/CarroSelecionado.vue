<script lang="ts">
import type { Carro } from '../../types/Carro/Carro'

export default {
  name: 'CarroSelecionado',

  props: {
    carro: {
      type: Object as () => Carro,
      required: true
    }
  }
}
</script>

<template>
  <div class="card m-2 tamanho-card">
    <div class="card-body">
      <h5 class="card-title ">
        Carro Selecionado
      </h5>
        <img
            src="https://images.unsplash.com/photo-1583121274602-3e2820c69888?q=80&w=870&auto=format&fit=crop"
            class="card-img-top img-fluid rounded-start h-100 object-fit-cover"
            alt="Imagem do carro"
        >
      <div class="row align-items-center">
        <div class="nomes d-flex justify-content-between">
          <h3 class="mb-1">
            {{ carro.marca }}
            {{ carro.modelo }}
          </h3>

          <p class="text-muted mt-2 mb-0">
            {{ carro.categoria }} · {{ carro.ano }}
          </p>
        </div>
        <div class="tags d-flex justify-content-between">

            <span class="badge bg-success">
                {{ carro.status }}
            </span>
        </div>

        <div class="valores d-flex flex-column align-items-start mt-3">
          <small class="text-muted">
            Valor da diária:
          </small>

          <div class="fs-4 fw-bold">
            R$
            {{ carro.valorDiaria.toFixed(2).replace('.', ',') }}

            <small class="fs-6 fw-normal">
              /dia
            </small>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
/* .tamanho-card{
    max-width: 600px;
} */
.card-img-top{
    /* max-width: 98%; */
    max-height: 256px;
}
</style>
