import React from 'react';
import {Card, CardContent, Typography} from '@mui/material';
import StatusDisplay from "./StatusDisplay";
import {ExportJob} from "../Models/ExportJob";

type JobCardProps = {
    job: ExportJob;
};

export const ExportJobCard: React.FC<JobCardProps> = ({job}) => {
    return (
        <Card>
            <CardContent>
                <Typography variant="h6">{job.name}</Typography>
                <StatusDisplay status={job.status}/>
                <Typography variant="body2" color="textSecondary">
                    Created At: {new Date(job.createdAt).toLocaleString()}
                </Typography>
            </CardContent>
        </Card>
    );
};