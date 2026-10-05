<template>
    <main class="container py-5 w-100">
        <!-- ==========================================
        TÍTULO
        =========================================== -->
        <h1 class="mb-4 flex justify-content-start">
            Nova Locação
        </h1>       
        <!-- ==========================================
        ETAPAS
        =========================================== -->
        <div class="mb-4">
          <EtapasLocacao 
          :classe-cabecalho="{ 'max-width': '187.5rem' }"
          :visivel="carro !== null" 
          :carroId=" carro ? carro?.id : -1" />
        </div>
        <!-- Carregando -->
        <div
        v-if="carregando"
        class="text-center"
        >
            <div
            class="spinner-border"
            role="status"
            >
                <span class="visually-hidden">
                    Carregando...
                </span>
            </div>
            
            <p class="mt-3">
                Carregando informações do carro...
            </p>
        </div>

        <!-- Erro -->
        <div
        v-else-if="erro"
        class="alert alert-danger"
        role="alert"
        >
            {{ erro }}
            <div class="mt-3">
                <button
                type="button"
                class="btn btn-secondary"
                @click="voltarParaCarros"
                >
                    Voltar para detalhes do carro
                </button>
            </div>
        </div>
        <!-- Carro -->
        <div v-else-if="carro">
          <!-- ========================================
          CARRO SELECIONADO
          ========================================= -->       
          <div class="conteudo d-flex tamanho">
              <div class="direita col-6">
                  <CarroSelecionado :carro="carro" />
              </div>
              <div class="esquerda col-6 p-2 mb-2">
                  <DefinirPeriodo 
                      v-model:dataInicio="dataInicio" 
                      v-model:dataFim="dataFim" 
                      v-model:localRetirada="localRetirada" 
                      :erro-periodo="erroPeriodo" 
                  /> 
                  <ResumoLocacao 
                      :quantidade-dias="quantidadeDiasLocacao" 
                      :valor-diaria="carro.valorDiaria" 
                      :valor-total="valorTotalLocacao" 
                  /> 
              </div>
          </div>
          <!-- ========================================
          BOTÕES
          ========================================= -->
          <!-- TODO: REMOVER ESSA DIV DEPOIS 
          <span>dados periodo</span>
          <div class="d-flex justify-content-between tamanho">
                      <p>erroPeriodo: {{ erroPeriodo }}</p>
                      <p>dataInicio: {{ dataInicio }}</p>
                      <p>dataFim: {{ dataFim }}</p>
                      <p>localRetirada: {{ localRetirada }}</p>
          </div>    
          -->
        <div class="d-flex justify-content-between tamanho">
          <button
            type="button"
            class="btn btn-outline-secondary"
            @click="voltarParaCarro"
          >
          Voltar
          </button>
          <button
            type="button"
            class="btn btn-primary"
            :disabled="
            !dataInicio ||
            !dataFim ||
            !!erroPeriodo ||
            !localRetirada
            "
            @click="continuarLocacao"
          >
            Confirmar Locação
          </button>
        </div>
  </div>

</main>
</template>
<script lang="ts">
import DefinirPeriodo from '../components/locacao/DefinirPeriodo.vue'
import ResumoLocacao from '../components/locacao/ResumoLocacao.vue'
import CarroSelecionado from '../components/carro/CarroSelecionado.vue'
import EtapasLocacao from '../components/locacao/EtapasLocacao.vue'
import carroService from '../services/carroService'
import type { Carro } from '../types/Carro/Carro'
import {
  quantidadeDias,
  valorTotal,
  formatarData
} from '../utils/locacaoUtils'; 
export default {
  name: 'LocacaoView',

  components: {
    DefinirPeriodo,
    ResumoLocacao,
    CarroSelecionado,
    EtapasLocacao,
  },

  data() {
    return {
      dataInicio: null as Date | null,
      dataFim: null as Date | null,
      carro: null as Carro | null,
      carregando: true,
      erro: '',
      localRetirada: ''
    }
  },

  computed: {
    erroPeriodo(): string {
      if (!this.dataInicio || !this.dataFim) return ''
      if (this.dataFim < this.dataInicio) return 'A data de devolução deve ser posterior à data de retirada.'
      return ''
    },

    quantidadeDiasLocacao(): number {
      if (!this.dataInicio || !this.dataFim) {
        return 0
      }

      return quantidadeDias(
        this.dataInicio,
        this.dataFim
      );
    },

    valorTotalLocacao(): number {
      if (!this.carro) {
        return 0
      }
      return valorTotal(
        this.quantidadeDiasLocacao,
        this.carro.valorDiaria
      );
    }
  },

  async mounted() {
    try {
      const id = Number(this.$route.params.id)

      if (isNaN(id)) {
        this.erro = 'ID do carro inválido.'
        return
      }

      this.carro = await carroService.getById(id)
    } catch (error) {
      this.carregando = false
      console.error(error)
      this.erro = 'Não foi possível carregar os dados do carro.'
    } finally {
      this.carregando = false
    }
  },

  methods: {
   formatarData,
    voltarParaCarro() {
      const id = Number(this.$route.params.id)

      this.$router.push(`/carros/${id}`)
    },

    continuarLocacao() {
      //erro periodo é computed
      //então não precisa verificar novamente aqui

      const locacao = {
        carro: this.carro,
        dataInicio: this.formatarData(this.dataInicio),
        dataFim: this.formatarData(this.dataFim),
        localRetirada: this.localRetirada
      };
      console.log('locacao:', locacao);
      sessionStorage.setItem(
        'locacaoPendente',
        JSON.stringify(locacao)
      );
/*
      console.log({
        carroId: this.carro.id,
        dataInicio: this.dataInicio,
        dataFim: this.dataFim,
        localRetirada: this.localRetirada,
        quantidadeDias: this.quantidadeDiasLocacao,
        valorTotal: this.valorTotalLocacao
      })
*/
      this.$router.push('/confirmacao-locacao');
    }
  }
}
</script>

<style scoped lang="scss">
.tamanho{
    max-width: 187.5rem;// 3000 pixels
    //background-color: var(--color-red); 
}
// .data-inicio,.data-fim{
//     display: flex;
//     justify-content: flex-start !important;
//     align-items: start;
//     flex-direction: column;
// }
</style>