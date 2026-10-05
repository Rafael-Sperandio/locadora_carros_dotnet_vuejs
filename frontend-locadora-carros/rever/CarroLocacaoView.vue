

<template>
    <main class="container py-5 w-100">
        
        <!-- ==========================================
        TÍTULO
        =========================================== -->
        
        <h1 class="mb-4">
            Nova Locação
        </h1>
        
        
        <!-- ==========================================
        ETAPAS
        =========================================== -->
        
        <div class="mb-4">
            
            <div class="tamanho">
                
                <div class="row text-center">
                    
                    <div class="col">
                        <strong>
                            1. Selecionar Carro
                        </strong>
                    </div>
                    
                    <div class="col">
                        <strong class="text-primary">
                            2. Período da Locação
                        </strong>
                    </div>
                    
                    <div class="col text-muted">
                        3. Confirmação
                    </div>
                    
                </div>
                
            </div>
            
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
        <div
        v-else-if="carro"
        >

        <!-- ========================================
        CARRO SELECIONADO
        ========================================= -->
                
        <div class="conteudo d-flex tamanho">
            <div class="direita">
                <div class="card m-2">
                    
                    <div class="card-body ">
                        
                        <h5 class="card-title">
                            Carro Selecionado
                        </h5>
                        
                        <div class="row align-items-center">
                            
                            <div class="col-md-6">
                                
                                <h3 class="mb-1">
                                    {{ carro.marca }}
                                    {{ carro.modelo }}
                                </h3>
                                
                                <span class="badge bg-success">
                                    {{ carro.status }}
                                </span>
                                
                                <p class="text-muted mt-2 mb-0">
                                    {{ carro.categoria }} · {{ carro.ano }}
                                </p>
                                
                            </div>
                            
                            <div class="col-md-6 text-md-end">
                                
                                <small class="text-muted">
                                    Valor da diária
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
            </div>
            
            
            <div class="esquerda p-2 mb-2">
                
                <div class="card periodo-locacao p-3 mb-2">
                    <div class="d-flex justify-content-start">
                        <h6 class="">Definir Período</h6>
                        
                    </div>
                    <div class="datas d-flex justify-content-between">
                        <div class="data-inicio ">
                            <small>Data de inicio(Retirada)</small>
                            <VueDatePicker
                            v-model="dataInicio"
                            placeholder="Selecione a data de inicio"
                            />
                        </div>
                        <div class="data-fim">
                            <small>Data de Fim (Devolução)</small>
                            <VueDatePicker
                            v-model="dataFim"
                            placeholder="Selecione a data de fim"
                            />
                        </div>
                        
                    </div>
                    <div class="local-retirada">
                        <small>Local de Retirada / Devolução</small>
                        <select class="form-select">
                            <option>Selecione</option>
                            <option>Aeroporto de Congonhas (Saguão Central)</option>
                        </select>
                    </div>
                </div>
                
                
                <!-- ========================================
                RESUMO
                ========================================= -->
                
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
                                Diárias
                                ({{ quantidadeDias }}x R$
                                {{ carro.valorDiaria.toFixed(2).replace('.', ',') }}):
                            </span>
                            
                            <strong>
                                R$
                                {{ valorTotal.toFixed(2).replace('.', ',') }}
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
                                R$
                                {{ valorTotal.toFixed(2).replace('.', ',') }}
                            </strong>
                            
                        </div>
                        
                    </div>
                </div>
            </div>
        </div>


<!-- ========================================
BOTÕES
========================================= -->

<div class="d-flex justify-content-between">
    
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
@click="confirmarLocacao"
>
Confirmar Locação
</button>

</div>


</div>

</main>
</template>

<script setup lang="ts">
/*
Area Definir Período
Definir Período
Data de Início (Retirada)       Data de Fim (Devolução)
24/10/2024                      29/10/2024
Local de Retirada / Devolução
Aeroporto de Congonhas (Saguão Central)
*/
import { computed, onMounted, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'

import carroService from '../services/carroService'
import type { Carro } from '../types/Carro/Carro'

import { VueDatePicker } from '@vuepic/vue-datepicker'
import '@vuepic/vue-datepicker/dist/main.css'

const dataInicio = ref<Date | null>(null)
const dataFim = ref<Date | null>(null)

const route = useRoute()
const router = useRouter()

const carro = ref<Carro | null>(null)

const carregando = ref(true)
const erro = ref('')
const localRetirada = ref('')

onMounted(async () => {
    try {
        const id = Number(route.params.id)
        
        if (isNaN(id)) {
            erro.value = 'ID do carro inválido.'
            return
        }
        
        carro.value = await carroService.getById(id)
    } catch (error) {
        console.error(error)
        
        erro.value = 'Não foi possível carregar os dados do carro.'
    } finally {
        carregando.value = false
    }
})
/*
|--------------------------------------------------------------------------
| Cálculo da locação
|--------------------------------------------------------------------------
*/

const erroPeriodo = computed(() => {
    if (!dataInicio.value || !dataFim.value) {
        return ''
    }
    
    if (dataFim.value <= dataInicio.value) {
        return 'A data de devolução deve ser posterior à data de retirada.'
    }
    
    return ''
})


const quantidadeDias = computed(() => {
    if (!dataInicio.value || !dataFim.value) {
        return 0
    }
    
    if (dataFim.value <= dataInicio.value) {
        return 0
    }
    
    const diferenca =
    dataFim.value.getTime() -
    dataInicio.value.getTime()
    
    return Math.ceil(
    diferenca / (1000 * 60 * 60 * 24)
    )
})


const valorTotal = computed(() => {
    if (!carro.value) {
        return 0
    }
    
    return quantidadeDias.value * carro.value.valorDiaria
})


/*
|--------------------------------------------------------------------------
| Ações
|--------------------------------------------------------------------------
*/

function voltarParaCarro() {
    const id = Number(route.params.id)
    
    router.push(`/carros/${id}`)
}


function confirmarLocacao() {
    if (!carro.value) {
        return
    }
    
    if (!dataInicio.value || !dataFim.value) {
        erro.value = 'Selecione as datas da locação.'
        return
    }
    
    if (erroPeriodo.value) {
        return
    }
    
    if (!localRetirada.value) {
        erro.value = 'Selecione o local de retirada.'
        return
    }
    
    /*
    * Aqui futuramente você chamará:
    *
    * locacaoService.create(...)
    *
    * depois que tivermos o DTO do backend.
    */
    
    console.log({
        carroId: carro.value.id,
        dataInicio: dataInicio.value,
        dataFim: dataFim.value,
        localRetirada: localRetirada.value,
        quantidadeDias: quantidadeDias.value,
        valorTotal: valorTotal.value
    })
}
</script>

<style scoped lang="scss">
.tamanho{
    max-width: 2050px;
    //width: 90%;
}
.container1{
    background-color: var(--color-red);
}
.esquerda{
    background-color: var(--color-gray); 
}

.data-inicio,.data-fim{
    display: flex;
    justify-content: flex-start !important;
    align-items: start;
    flex-direction: column;
}
</style>