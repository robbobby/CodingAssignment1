export type ExportJobStatus = "Error" | "Processing" | "Completed";
export type ExportJob = {
    id: string;
    name: string;
    status: ExportJobStatus;
    createdAt: string;
};