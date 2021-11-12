<template>
    <div class="container" v-bind:class="{ self: self, other: !self }">
        <label>Name:</label>
        <input
            id="test"
            type="text"
            v-model="clientId"
            v-bind:disabled="!self"
        />
        <p>{{ self ? "Shared with me" : "Share with " + clientId }}:</p>
        <div class="share-link" v-bind:class="{ self: self, other: !self }">
            <input
                type="text"
                v-model="shareLink"
                v-on:change="invokeAddGribboardEntry()"
            />
            <input
                type="submit"
                v-on:click="invokeAddGribboardEntry()"
                value="send"
            />
        </div>
        <ul>
            <li v-for="(link, index) in links" v-bind:key="index">
                <div v-html="formatText(link)" />
            </li>
        </ul>
    </div>
</template>

<script>
export default {
    name: "Client",
    props: {
        clientId: String,
        self: Boolean,
    },
    data() {
        return {
            links: [],
            shareLink: "",
        };
    },
    created() {
        this.$gribboardHub.on("GribboardEntryAdded", this.AddGribboardEntry);
    },
    methods: {
        invokeAddGribboardEntry() {
            if (this.shareLink === undefined || this.shareLink == "") {
                return;
            }
            this.links.push(this.shareLink);
            this.$gribboardHub.invoke(
                "AddGribboardEntry",
                this.shareLink,
                this.clientId,
                false
            );
            this.shareLink = "";
        },
        AddGribboardEntry(link, autoOpen, fromId) {
            if (this.self) {
                this.links.push(link);
            }
        },
        formatText(text) {
            var urlRegex = /(https?:\/\/[^\s]+)/g;
            return text.replace(urlRegex, function (url) {
                return '<a href="' + url + '" target="_blank">' + url + "</a>";
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
    padding: 0;
}
li {
    margin: 0 10px;
}
a {
    color: #094d2e;
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
