<template>
  <div>
    <navBar></navBar>

    <main>
        <RouterView />
    </main>
  </div>

</template>

<script >
import navBar from './components/NavComponent.vue'
//import HelloWorld from './components/HelloWorld.vue'
export default {
  name: 'App',
  components: {
    navBar
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  /* -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale; */
  text-align: center;
  /* color: #2c3e50; */
  /* margin-top: 60px; */
}
</style>
