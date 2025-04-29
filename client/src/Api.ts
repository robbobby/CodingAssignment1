import axios from "axios";

axios.defaults.baseURL = 'http://localhost:5170'

const urls = {
    exportJobs: {
        Get: '/api/export-jobs',
        Start: '/api/export-jobs/start',
    },
}

type ExportJob = {
    id: string;
    name: string;
    status: string;
    createdAt: string;
};

export const api = {
    exportJobs: {
        get: async (): Promise<ExportJob[]> => {
            const response = await axios.get(urls.exportJobs.Get);
            return response.data;
        },
        start: async (jobId: string) => {
            const response = await axios.post(urls.exportJobs.Start, { jobId });
            return response.data;
        },
    },
};