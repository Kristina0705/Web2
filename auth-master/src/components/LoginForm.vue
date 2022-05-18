<template>
  <form v-if="!isAuthorized" id="login-form" v-on:submit="submit">
    <label for="username">Логин:</label>
    <input
      type="username"
      name="username"
      id="username"
      autocomplete="username"
      v-model="username"
    />
    <label for="password">Пароль:</label>
    <input
      type="password"
      name="password"
      id="password"
      autocomplete="password"
      v-model="password"
    />
    <button type="submit" value="login">Войти</button>
    <button type="submit" value="register">Зарегистрироваться</button>
  </form>
  <span v-if="isAuthorized" class="current-user">
    <span class="user-name">Здравствуйте, {{ username }}!</span>
    <button v-on:click="logout">Выход</button>
  </span>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import axios from "axios";
export default defineComponent({
  data() {
    return {
      username: "",
      password: "",
      isAuthorized: false,
    };
  },
  methods: {
    checkAuth: function () {
      const auth = localStorage.getItem("Authorization");
      if (!auth) {
        return;
      }
      axios
        .get("/current-user", { headers: { Authorization: auth } })
        .then(({ data }) => {
          this.username = data.username;
          this.isAuthorized = true;
        })
        .catch(() => localStorage.removeItem("Authorization"));
    },
    // eslint-disable-next-line no-undef
    submit: function (event: SubmitEvent) {
      event.preventDefault();
      event.stopPropagation();
      const type = (event.submitter as HTMLButtonElement).value;
      const username = this.username;
      switch (type) {
        case "login":
          axios
            .post("/login", {
              username,
              password: this.password,
            })
            .then((res) => {
              if (res.status !== 200) {
                throw new Error();
              }
              return res.data;
            })
            .then((auth) => {
              localStorage.setItem(
                "Authorization",
                `${auth.token_type} ${auth.access_token}`
              );
              this.username = auth.username;
              this.isAuthorized = true;
            })
            .catch((err) => {
              console.log(err);
              alert("Неправильный логин или пароль");
            });
          break;
        case "register":
          axios
            .post("/register", {
              username,
              password: this.password,
            })
            .then((res) => {
              if (res.status !== 200) {
                throw new Error();
              }
              alert(`Пользователь ${username} успешно создан`);
            })
            .catch(() => {
              alert("Не удалось создать пользователя " + this.username);
            });
          break;
      }
    },
    logout: function () {
      axios
        .post("/logout")
        .then(() => localStorage.removeItem("Authorization"))
        .catch(() => localStorage.removeItem("Authorization"));
      this.username = "";
      this.password = "";
      this.isAuthorized = false;
    },
  },
  beforeMount() {
    this.checkAuth();
  },
});
</script>

<style scoped>
#login-form {
  display: flex;
}
</style>
