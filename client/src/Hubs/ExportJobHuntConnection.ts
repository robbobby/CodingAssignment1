import * as signalR from "@microsoft/signalr";
import {ExportJobStatus} from "../Models/ExportJob";

let connection: signalR.HubConnection | null = null;

export function startJobHubConnection(onJobUpdate: (jobId: number, status: ExportJobStatus) => void) {
    if (connection) {
        return; // Already connected
    }

    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:5170/ws/exportJobHub")
        .withAutomaticReconnect()
        .build();

    connection.start()
        .then(() => {
            console.log("Connected to SignalR hub");

            connection?.on("JobUpdate", (jobId: number, status: ExportJobStatus) => {
                console.log(`JobUpdate received: Job ${jobId} Status ${status}`);
                onJobUpdate(jobId, status);
            });
        })
        .catch(error => console.error("SignalR connection error:", error));
}

export function stopJobHubConnection() {
    if (connection) {
        connection.stop();
        connection = null;
    }
}
