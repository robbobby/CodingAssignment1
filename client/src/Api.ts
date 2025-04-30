import axios from "axios";
import {ExportJob} from "./Models/ExportJob";

axios.defaults.baseURL = 'http://localhost:5170'

const urls = {
    exportJobs: {
        Get: '/api/export-jobs',
        Start: '/api/export-jobs/start',
    },
}

export const api = {
    exportJobs: {
        get: async (): Promise<ExportJob[]> => {
            const response = await axios.get(urls.exportJobs.Get);
            return response.data;
        },
        start: async (dataSet: string) => {
            const response = await axios.post(urls.exportJobs.Start, {dataSet});
            return response.data;
        },
    },
};