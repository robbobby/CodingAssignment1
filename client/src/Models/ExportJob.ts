export type ExportJobStatus = "Error" | "Processing" | "Completed";
export type ExportJob = {
    id: number;
    name: string;
    status: ExportJobStatus;
    createdAt: string;
};