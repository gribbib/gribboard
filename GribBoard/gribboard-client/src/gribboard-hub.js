import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr'


export default {
    install: (app, options) => {
        const connection = new HubConnectionBuilder()
            .withUrl('/gribboardHub')
            .configureLogging(LogLevel.Information)
            .build();
        app.config.globalProperties.$gribboardHub = connection
        app.provide('gribboard-hub', connection)
        connection.start();
    }
}