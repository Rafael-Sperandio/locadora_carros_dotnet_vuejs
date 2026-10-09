<!-- TODO: criar componente para confirmação de locação, que exiba os dados da locação e do cliente, e permita confirmar ou voltar para editar -->
<template>
  <div class="container py-4">

    <div class="mb-4">

      <h2 class="mt-2">Confirmação da locação</h2>

      <p class="text-muted">
        Confira os dados antes de confirmar sua reserva.
      </p>
    </div>

    <div v-if="locacao">

      <!-- Mensagem de revisão -->
      <div class="alert alert-info mb-4">
        <h5>Quase lá!</h5>
        Confira os dados da sua locação antes de finalizar.
      </div>

      <div class="row g-4">

        <!-- Resumo da locação -->
        <div class="col-lg-7">

          <div class="card p-4 mb-4">

            <h5 class="mb-3">Veículo selecionado</h5>

            <div class="d-flex gap-3 align-items-center">

              <img
                :src="locacao.carro.imagem"
                alt="Imagem do veículo"
                class="carro-imagem"
              />

              <div>
                <h5>
                  {{ locacao.carro.marca }}
                  {{ locacao.carro.modelo }}
                </h5>

                <p class="text-muted mb-1">
                  {{ locacao.carro.categoria }}
                  • {{ locacao.carro.ano }}
                </p>

                <strong>
                  {{ formatarMoeda(locacao.carro.valorDiaria) }}
                  <small>/ diária</small>
                </strong>
              </div>

            </div>

            <hr />

            <h5 class="mb-3">Período da locação</h5>

            <div class="row g-3">

              <div class="col-md-6">
                <small class="text-muted">Retirada</small>
                <p class="fw-bold">
                  {{ formatarData(locacao.dataInicio) }}
                </p>
              </div>

              <div class="col-md-6">
                <small class="text-muted">Devolução</small>
                <p class="fw-bold">
                  {{ formatarData(locacao.dataFim) }}
                </p>
              </div>

              <div class="col-md-6">
                <small class="text-muted">Quantidade de diárias</small>
                <p class="fw-bold">
                  {{ quantidadeDiasLocacao }} dias
                </p>
              </div>

              <div class="col-md-6">
                <small class="text-muted">Local de retirada</small>
                <p class="fw-bold">
                  {{ locacao.localRetirada }}
                </p>
              </div>

            </div>

          </div>

          <!-- Dados do cliente -->
          <div class="card p-4">

            <h5 class="mb-3">Dados do cliente</h5>

            <div class="mb-2">
              <small class="text-muted">Nome completo</small>
              <p class="fw-bold">{{ cliente.nome }}</p>
            </div>

            <div class="mb-2">
              <small class="text-muted">E-mail</small>
              <p class="fw-bold">{{ cliente.email }}</p>
            </div>

            <div>
              <small class="text-muted">Telefone</small>
              <p class="fw-bold">{{ cliente.telefone }}</p>
            </div>

          </div>

        </div>

        <!-- Resumo financeiro -->
        <div class="col-lg-5">

          <div class="card p-4">

            <h5 class="mb-4">Resumo do pagamento</h5>

            <div class="d-flex justify-content-between mb-3">
              <span>Valor da diária</span>
              <strong>
                {{ formatarMoeda(locacao.carro.valorDiaria) }}
              </strong>
            </div>

            <div class="d-flex justify-content-between mb-3">
              <span>Quantidade de diárias</span>
              <strong>{{ quantidadeDiasLocacao }}</strong>
            </div>

            <hr />

            <div class="d-flex justify-content-between mb-4">
              <h5>Valor total</h5>

              <h5 class="valor-total">
                {{ formatarMoeda(valorTotalLocacao) }}
              </h5>
            </div>

            <div class="alert alert-secondary">
              O pagamento será realizado conforme as condições
              estabelecidas pela locadora.
            </div>

            <div
              v-if="erro"
              class="alert alert-danger"
            >
              {{ erro }}
            </div>

            <button
              class="btn btn-primary w-100 mb-2"
              :disabled="carregando"
              @click="confirmarLocacao"
            >
              {{ carregando ? 'Confirmando...' : 'Confirmar locação' }}
            </button>

            <button
              class="btn btn-outline-secondary w-100"
              :disabled="carregando"
              @click="voltar"
            >
              Voltar e editar
            </button>

          </div>

        </div>

      </div>

    </div>

  </div>
</template>
<script lang="ts">
import {
  formatarData,
  formatarMoeda,
  quantidadeDias,
  valorTotal
} from '../utils/locacaoUtils'; 
export default {
  name: 'ConfirmacaoLocacao',

  data() {
    return {
      locacao: null as any,

      // Cliente fixo provisório
      cliente: {
        id: 1,
        nome: 'Rafael Sperandio',
        email: 'rafael@email.com',
        telefone: '(47) 99999-9999'
      },

      carregando: false,

      erro: ''
    };
  },

  computed: {
 
    quantidadeDiasLocacao(): number {
      if (!this.locacao) return 0;
      return quantidadeDias(
        this.locacao.dataInicio,
        this.locacao.dataFim
      );
    },

    valorTotalLocacao(): number {
      if (!this.locacao) return 0;

      return valorTotal(
        this.quantidadeDiasLocacao,
        this.locacao.carro.valorDiaria
      );
    }
  },
  mounted() {
    this.carregarLocacao();
  },

  methods: {
    formatarData,
    formatarMoeda,
    carregarLocacao() {
      const dados = sessionStorage.getItem('locacaoPendente');

      if (!dados) {
        this.$router.replace('/carros');
        return;
      }

      try {
        this.locacao = JSON.parse(dados);
      } catch {
        this.erro = 'Não foi possível carregar os dados da locação.';
      }
    },



    voltar() {
      this.$router.back();
    },

    async confirmarLocacao() {
      if (!this.locacao || this.carregando) return;

      this.carregando = true;
      this.erro = '';

      try {
        // Futuramente, aqui será feita a requisição
        // POST para o backend.

        const payload = {
          clienteId: this.cliente.id,
          carroId: this.locacao.carro.id,
          dataInicio: this.locacao.dataInicio,
          dataFim: this.locacao.dataFim
        };

        console.log('Dados da locação:', payload);

        /*
        const response = await api.post('/Locacao', payload);

        sessionStorage.removeItem('locacaoPendente');

        this.$router.push({
          name: 'MinhasLocacoes'
        });
        */

        // Temporário: simula a confirmação visual
        alert('Locação confirmada com sucesso!');

      } catch (error) {
        this.erro = 'Não foi possível confirmar sua locação.';
      } finally {
        this.carregando = false;
      }
    }
  }
};
</script>
<style scoped lang="scss">

.carro-imagem {
  width: 180px;
  height: 115px;
  object-fit: cover;
  border-radius: 8px;
}

.valor-total {
  color: #FF3F4B;
}

.card {
  border-radius: 12px;
}

</style>