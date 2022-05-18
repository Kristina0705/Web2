<template>
  <login-form></login-form>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import LoginForm from "./components/LoginForm.vue";

export default defineComponent({
  name: "App",
  components: {
    LoginForm,
  },
});
</script>

<style>
* {
  padding: 0;
  margin: 0;
}
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
}
</style>
