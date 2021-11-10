<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    <h1 v-if="test">Me - {{ test }}</h1>
    <Client v-for="(item, index) in items" :name="item.message" />
  </div>
</template>

<script>
import Client from './Client.vue'

export default {
  name: 'GribBoard',
  components: {
    Client
  },
  props: {
    msg: String,
  },
  data() {
    return {
      test: "",
      items: [],
    };
  },
  created() {
    this.$gribboardHub.on("ClientAdded", this.AddClient);
    this.$gribboardHub.on("MeAdded", this.AddMe);
  },
  methods: {
    AddClient(id) {
      this.items.push({ message: id });
    },
    AddMe(id, otherIds) {
      this.test = id;
      otherIds.forEach((otherId) => {
        if (otherId != id) {
          this.AddClient(otherId);
        }
      });
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
  list-style-type: none;
  padding: 0;
}
li {
  /* display:  inline-block; */
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
