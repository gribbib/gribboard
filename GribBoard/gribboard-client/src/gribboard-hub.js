import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr'


export default {
    install: (app, options) => {
        const connection = new HubConnectionBuilder()
            .withUrl('https://localhost:5051/gribboardHub')
            .configureLogging(LogLevel.Information)
            .build();
        app.config.globalProperties.$gribboardHub = connection
        app.provide('gribboard-hub', connection)
        connection.start();
    }
}