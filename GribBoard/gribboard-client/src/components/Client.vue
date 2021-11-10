<template>
  <div class="container" v-bind:class="{ self: self, other: !self }">
    <label>Name:</label>
    <input id="test" type="text" v-model="clientId" v-bind:disabled="!self" />
    <p>{{ self ? "Shared with me" : "Share with " + clientId }}:</p>
    <div class="share-link" v-bind:class="{ self: self, other: !self }">
      <input type="text" v-model="shareLink" />
      <input type="submit" v-on:click="test()" value="send" />
    </div>
    <ul>
      <li v-for="(link, index) in links" v-bind:key="index">{{ link }}</li>
    </ul>
  </div>
</template>

<script>
export default {
  name: "Client",
  props: {
    clientId: String,
    self: Boolean,
    links: [String],
  },
  data() {
    return {
      //   links: ["dfsf"],
      shareLink: "",
    };
  },
  created() {},
  methods: {
    test() {
        alert("test")
    },
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  padding: 0;
}
li {
  margin: 0 10px;
}
a {
  color: #42b983;
}

.share-link.self {
  display: none;
}
.container.self {
  background-color: #42b983;
}

.container.other {
  background-color: aquamarine;
}
.container {
  border-radius: 5px;
  padding: 5px;
  margin-bottom: 10px;
}
</style>
