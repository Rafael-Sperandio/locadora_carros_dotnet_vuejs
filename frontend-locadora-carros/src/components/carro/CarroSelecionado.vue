<script lang="ts">
import type { Carro } from '../../types/Carro/Carro'

export default {
  name: 'CarroSelecionado',

  props: {
    carro: {
      type: Object as () => Carro,
      required: true
    },
    titulo: {
      type: String,
      default: 'Carro Selecionado:'
    },
    valorVisivel: {
      type: Boolean,
      default: true
    }
  }
}
</script>

<template>
  <div class="card m-2 tamanho-card">
    <div class="card-body">
      <h5 v-if="!(titulo || titulo == '' )" class="card-title d-flex align-items-start">
        {{ titulo }}
      </h5>
        <img
            src="https://images.unsplash.com/photo-1583121274602-3e2820c69888?q=80&w=870&auto=format&fit=crop"
            class="card-img-top img-fluid rounded-start h-100 object-fit-cover"
            alt="Imagem do carro"
        >
      <div class="row align-items-center">
        <div class="nomes d-flex justify-content-between mt-1">
          <h3 class="mb-1">
            {{ carro.marca }}
            {{ carro.modelo }}
          </h3>

          <div class=" d-flex align-items-center">
            <span class="badge bg-success">
                {{ carro.status }}
            </span>
          </div>

        </div>
        <ul class="list-group list-group-flush">
          <li class="list-group-item d-flex justify-content-between align-items-center">
            <small class="text-muted">
              Placa:
            </small>

            <span>
              {{ carro.placa }}
            </span>
          </li>
          <li class="list-group-item d-flex justify-content-between align-items-center">
            <small class="text-muted">
              Informações:
            </small>
            <p class="d-flex mb-0">
              {{ carro.categoria }} · {{ carro.ano }}
            </p>
          </li>
          <li v-if="valorVisivel"  class="valores d-flex justify-content-between align-items-center px-3 mt-1">
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
          </li>
        </ul>
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
