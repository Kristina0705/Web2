<template>
  <FunctionGraph
    :function="
      (x) =>
        3 * x * x * x -
        2 * Math.exp(x * x * x - 2 * x * x - 1) +
        Math.log(x) / Math.log(13)
    "
    :x1="1"
    :x2="2.7"
    :dx="0.1"
    :max="40"
    :min="-40"
    :label="'3x^3 + 2e^(x^3 - 2x^2 - 1) + log13(x)'"
  />
  <UsersGrid />
</template>

<script lang="ts">
import { defineComponent } from "vue";
import FunctionGraph from "./components/FunctionGraph.vue";
import UsersGrid from "./components/UsersGrid.vue";

export default defineComponent({
  name: "App",
  components: {
    FunctionGraph,
    UsersGrid,
  },
});
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
