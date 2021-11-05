import { createApp } from 'vue'
import App from './App.vue'

import GribboardHub from './gribboard-hub'

const app = createApp(App);

app.use(GribboardHub)
app.mount('#app')
