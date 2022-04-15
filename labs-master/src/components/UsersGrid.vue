<template>
  <table>
    <tr>
      <th>ФИО</th>
      <th>email</th>
    </tr>
    <tr v-for="user of users" :key="user.email" @click="currentUser = user">
      <td>{{ user.name }}</td>
      <td>{{ user.email }}</td>
    </tr>
  </table>
  <UserModal
    v-if="!!currentUser"
    :user="currentUser"
    @close="currentUser = null"
  />
</template>

<script lang="ts">
import { defineComponent } from "vue";
import UserModal from "./UserModal.vue";
import axios from "axios";

export type User = {
  name: string;
  birthdate: Date;
  old: number;
  email: string;
};

export default defineComponent({
  components: { UserModal },
  data() {
    return {
      users: [] as User[],
      currentUser: null,
    };
  },
  mounted() {
    axios
      .get("https://5a313e56-bcd8-4e93-806b-d7325a8e51ac.mock.pstmn.io/users")
      .then(
        (response) =>
          (this.users = response.data.map((user: User) => {
            user.birthdate = new Date(user.birthdate);
            return user;
          }))
      );
  },
});
</script>

<style scoped>
table {
  border: 2px solid #42b983;
  border-radius: 3px;
  background-color: #fff;
}

th {
  background-color: #42b983;
  color: rgba(255, 255, 255, 0.66);
}

td {
  background-color: #f9f9f9;
  cursor: pointer;
}

th,
td {
  min-width: 120px;
  padding: 10px 20px;
  user-select: none;
}
</style>
