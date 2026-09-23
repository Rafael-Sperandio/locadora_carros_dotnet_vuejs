<template>
    <div>
        <nav class="navbar navbar-expand-lg navbar-light  marginPrincipal" >
        <!-- <nav class="marginPrincipal"> -->
            <ul class="navbar-nav mr-auto">
                <li class="nav-item active">
                    <RouterLink class="nav-link" to="/">Início</RouterLink>
                </li>
                <li class="nav-item active">
                    <RouterLink  class="nav-link" to="/clientes">Clientes</RouterLink>
                </li>

                <li class="nav-item active">
                    <RouterLink  class="nav-link" to="/carros">Carros</RouterLink>
                </li>

                <li class="nav-item active">
                    <RouterLink  class="nav-link" to="/locacoes">Locações</RouterLink>
                </li>
                
            </ul>

        </nav>
    </div>
</template>

<script>
    export default {
        
    }
</script>

<style scoped lang="scss">

nav {
    //color-background
    background-color: var(--color-navbar) ;
    /* padding: 20px 20px 20px 0px; */
    a {
        /* padding: 10px;
        text-decoration: none;
        background-color: white;
        border-radius: 3px;
        color: rgb(0, 110, 255);
        font-weight: bold;
        margin-right: 15px ; */
    }
    .router-link-active{
        /* background-color: rgb(160, 160, 160);
        color: white; */
    }
}


</style>