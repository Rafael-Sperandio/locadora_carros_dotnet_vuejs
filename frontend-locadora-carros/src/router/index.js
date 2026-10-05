import { createRouter, createWebHistory } from 'vue-router'

import HomeView from '../views/HomeView.vue'
import ClientesView from '../views/ClientesView.vue'
import CarrosView from '../views/CarrosView.vue'
import CarroDetalhesView from '../views/CarroDetalhesView.vue'
import CarroLocacaoView from '../views/CarroLocacaoView.vue'
import LocacoesView from '../views/LocacoesView.vue'
import ConfirmacaoLocacao from '../views/ConfirmacaoLocacao.vue'
const routes = [
    {
        path: '/',
        name: 'home',
        component: HomeView
    },
    {
        path: '/clientes',
        name: 'clientes',
        component: ClientesView
    },
    {
        path: '/carros',
        name: 'carros',
        component: CarrosView
    },
    { 
        path: '/carros/:id', 
        name: 'carro-detalhes', 
        component: CarroDetalhesView 
    },   
        { 
        path: '/carros/:id/locacao', 
        name: 'carro-locacao', 
        component: CarroLocacaoView
    },
    { 
        path: '/confirmacao-locacao', 
        name: 'ConfirmacaoLocacao', 
        component: ConfirmacaoLocacao
    },  
    {
        path: '/locacoes',
        name: 'locacoes',
        component: LocacoesView
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router