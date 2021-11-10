<template>
  <div>
    <GribBoard msg="Grib Board Client"/>
  </div>
</template>

<script>
import GribBoard from './components/GribBoard.vue'

export default {
  name: 'App',
  components: {
    GribBoard
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
