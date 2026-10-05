<template>
    <!-- Carro -->
    <div class="card shadow-sm">
      <div class="row g-0">
        <!-- Imagem -->
        <div class="col-md-5">
          <img
            src="https://images.unsplash.com/photo-1583121274602-3e2820c69888?q=80&w=870&auto=format&fit=crop"
            class="img-fluid rounded-start h-100 object-fit-cover"
            alt="Imagem do carro"
          >
        </div>
        <!-- Informações -->
        <div class="col-md-7">

          <div class="card-body p-4">
            <div class="mb-4">
              <h6 class="text-muted mb-1">
                {{ carro.marca }}
              </h6>
              <h2 class="card-title mb-0">
                {{ carro.marca }} {{ carro.modelo }}
              </h2>
            </div>
            <!-- Informações básicas -->
            <h5 class="mb-3">
              Informações
            </h5>

            <div class="row g-3 mb-4">

              <div class="col-sm-6">
                <div class="border rounded p-3 h-100">
                  <small class="text-muted">
                    Ano
                  </small>
                  <div class="fw-semibold">
                    {{ carro.ano }}
                  </div>
                </div>
              </div>

              <div class="col-sm-6">
                <div class="border rounded p-3 h-100">
                  <small class="text-muted">
                    Categoria
                  </small>
                  <div class="fw-semibold">
                    {{ carro.categoria }}
                  </div>
                </div>
              </div>

              <div class="col-sm-6">
                <div class="border rounded p-3 h-100">
                  <small class="text-muted">
                    Placa
                  </small>
                  <div class="fw-semibold">
                    {{ carro.placa }}
                  </div>
                </div>
              </div>

              <div class="col-sm-6">
                <div class="border rounded p-3 h-100">
                  <small class="text-muted">
                    Status
                  </small>
                  <div class="fw-semibold">
                    {{ carro.status }}
                  </div>
                </div>
              </div>

            </div>
            <!-- Valor -->
            <div class="border-top pt-4 mb-4">
              <small class="text-muted">
                Valor da diária
              </small>
              <div class="fs-3 fw-bold">
                R$ {{ carro.valorDiaria }}
                <span class="fs-6 fw-normal text-muted">
                  / dia
                </span>
              </div>
            </div>
            <!-- Ações -->
            <div class="d-flex gap-2">
              <button
                type="button"
                class="btn btn-outline-secondary"
                @click="voltarParaCarros"
              >
                Voltar
              </button>
                <!-- :disabled="carro.status !== 'Disponível'" -->

              <button
                type="button"
                class="btn btn-primary flex-grow-1"
                @click="alugarCarro"
              >
                Alugar este carro
              </button>
            </div>
          </div>

        </div>

      </div>

    </div>
</template>

<script lang="ts">
import type { Carro } from '../../types/Carro/Carro'
import { useRouter } from 'vue-router'

export default {
  name: 'CarroDetalhe',

  props: {
    carro: {
      type: Object as () => Carro,
      required: true
    }
  },

  setup() {
    const router = useRouter()

    return {
      router
    }
  },

  methods: {
    voltarParaCarros() {
      this.router.push('/carros')
    },

    alugarCarro() {
      const id = this.carro.id
      this.router.push(`/carros/${id}/locacao`)
    }
  }
}
</script>

