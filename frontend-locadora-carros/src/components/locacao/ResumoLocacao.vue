
<script lang="ts">
export default {
  name: 'ResumoLocacao',
  props: {
    quantidadeDias: {
      type: Number,
      required: true
    },

    valorDiaria: {
      type: Number,
      required: true
    },

    valorTotal: {
      type: Number,
      required: true
    }
  }
}
</script>

<template>
  <div class="card resumo-locacao">
    <div class="card-body">
      <h5 class="card-title mb-2">
        Resumo da Nova Locação
      </h5>

      <div class="resumo-linha">
        <span>
          Período de locação total:
        </span>

        <strong>
          {{ quantidadeDias }} Dias
        </strong>
      </div>

      <div class="resumo-linha">
        <span>
          Diárias ({{ quantidadeDias }}x R$ {{ valorDiaria.toFixed(2).replace('.', ',') }}):
        </span>

        <strong>
          R$ {{ valorTotal.toFixed(2).replace('.', ',') }}
        </strong>
      </div>

      <div class="resumo-linha">
        <span>
          Taxas de Serviço:
        </span>

        <strong>
          Isento
        </strong>
      </div>

      <hr>

      <div class="resumo-linha total">
        <span>
          Total estimado:
        </span>

        <strong>
          R$ {{ valorTotal.toFixed(2).replace('.', ',') }}
        </strong>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
.resumo-linha {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 8px 0;
}

.resumo-linha.total {
  font-size: 1.1rem;
}
</style>

