<template>
  <div class="card periodo-locacao p-3 mb-2">
    <h6>
      Definir Período
    </h6>

    <div class="datas d-flex justify-content-between">
      <div class="data-inicio">
        <small>
          Data de início (Retirada)
        </small>
        <VueDatePicker
        :model-value="dataInicio"
          @update:model-value="$emit('update:dataInicio', $event)" 
          placeholder="Selecione a data de início"
          :enable-time-picker="false"
          :formats="{ input: format }"
          auto-apply
        />
      </div>

      <div class="data-fim">
        <small>
          Data de fim (Devolução)
        </small>

        <VueDatePicker
          :model-value="dataFim"
          @update:model-value="$emit('update:dataFim', $event)"
          placeholder="Selecione a data de fim"
          :enable-time-picker="false"
          :formats="{ input: format }"
          auto-apply
        />
      </div>
    </div>

    <div
      v-if="erroPeriodo"
      class="alert alert-danger mt-3 mb-0"
    >
      {{ erroPeriodo }}
    </div>

    <div class="local-retirada mt-3">
      <small>
        Local de Retirada / Devolução
      </small>

      <select
        :value="localRetirada"
        @change="$emit('update:localRetirada', $event.target.value)"
        class="form-select"
      >
        <option value="" disabled>
          Selecione
        </option>

        <option value="aeroporto-congonhas">
          Aeroporto de Congonhas (Saguão Central)
        </option>
      </select>
    </div>
  </div>
</template>

<script lang="ts">
import { VueDatePicker } from '@vuepic/vue-datepicker'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  name: 'DefinirPeriodo',

  components: {
    VueDatePicker
  },

  props: {
    dataInicio: {
    type: Date,
    default: null
    },

    dataFim: {
    type: Date,
    default: null
    },
    
    localRetirada: {
      type: String,
      default: ''
    },

    erroPeriodo: {
      type: String,
      default: ''
    }
  },

  emits: [
    'update:dataInicio',
    'update:dataFim',
    'update:localRetirada'
  ],
    methods: {
    format(date: Date) {
      const day = String(date.getDate()).padStart(2, '0')
      const month = String(date.getMonth() + 1).padStart(2, '0')
      const year = date.getFullYear()
      return `${day}/${month}/${year}`
    }
  }
}
</script>