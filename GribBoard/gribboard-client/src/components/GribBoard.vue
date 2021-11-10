<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    <Client :clientId="myId" :self="true" :links="mySharedLinks"/>
    <Client v-for="(item, index) in clients" :clientId="item.message" :self="false" v-bind:key="index"/>
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
      myId: "",
      clients: [],
      mySharedLinks: []
    };
  },
  created() {
    this.$gribboardHub.on("ClientAdded", this.AddClient);
    this.$gribboardHub.on("MeAdded", this.AddMe);
    this.$gribboardHub.on("GribboardEntryAdded", this.AddGribboardEntry);
  },
  methods: {
    AddClient(id) {
      this.clients.push({ message: id });
    },
    AddMe(id, otherIds) {
      this.myId = id;
      otherIds.forEach((otherId) => {
        if (otherId != id) {
          this.AddClient(otherId);
        }
      });
    },
    AddGribboardEntry(link, autoOpen, fromId) {
      mySharedLinks.push(link)
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
