import React, { createContext, useContext, useEffect, useState } from 'react';
import { api } from './Api';
import { startJobHubConnection, stopJobHubConnection } from './Hubs/ExportJobHuntConnection';
import { ExportJob } from './Models/ExportJob';

type ExportJobContextType = {
    jobs: ExportJob[] | null;
    setJobs: (jobs: ExportJob[] | null) => void;
    loading: boolean;
    error: string | null;
    refreshJobs: () => void;
    startJob: (dataSet: string) => void;
};

const ExportJobContext = createContext<ExportJobContextType | undefined>(undefined);

export const ExportJobProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const [exportJobs, setExportJobs] = useState<ExportJob[] | null>(null);
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    const setJobs = (jobs: ExportJob[] | null) => {
        setExportJobs(jobs);
    };

    const fetchJobs = async () => {
        setLoading(true);
        setError(null);
        try {
            const data = await api.exportJobs.get();
            setExportJobs(data);
        } catch (err) {
            setError('Failed to fetch jobs');
        } finally {
            setLoading(false);
        }
    };

    const startJob = async (dataSet: string) => {
        setError(null);
        try {
            await api.exportJobs.start(dataSet);
            fetchJobs();
        } catch (err) {
            setError('Failed to start job');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchJobs();

        startJobHubConnection((jobId, status) => {
            setExportJobs((prevJobs) => {
                if (!prevJobs) return null;
                const updatedJobs = prevJobs.map((job) => {
                    if (job.id === jobId) {
                        return { ...job, status };
                    }
                    return job;
                });
                return updatedJobs;
            });
        });

        return () => {
            stopJobHubConnection();
        };
    }, []);

    return (
        <ExportJobContext.Provider value={{
            jobs: exportJobs,
            loading,
            error,
            refreshJobs: fetchJobs,
            startJob,
            setJobs
        }}>
            {children}
        </ExportJobContext.Provider>
    );
};

export const useExportJobContext = (): ExportJobContextType => {
    const context = useContext(ExportJobContext);
    if (!context) {
        throw new Error('useExportJobContext must be used within a ExportJobProvider');
    }
    return context;
};
