import { createRouter, createWebHistory } from 'vue-router'

import HomeView from '../views/HomeView.vue'
import ClientesView from '../views/ClientesView.vue'
import CarrosView from '../views/CarrosView.vue'
import LocacoesView from '../views/LocacoesView.vue'

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