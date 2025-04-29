import React from 'react';
import {Typography, Box} from '@mui/material';
import WarningIcon from '@mui/icons-material/Warning';
import {ExportJobStatus} from "../Models/ExportJob";

type StatusDisplayProps = {
    status: ExportJobStatus
};

const StatusDisplay: React.FC<StatusDisplayProps> = ({status}) => {
    let color: string;
    let icon: React.ReactNode = null;

    switch (status.toLowerCase()) {
        case "error":
            color = "red";
            icon = <WarningIcon style={{color: "red", marginRight: "0.5rem"}}/>;
            break;
        case "success":
            color = "green";
            break;
        default:
            color = "black";
    }

    return (
        <Box display="flex" alignItems="center">
            {icon}
            <Typography variant="body2" style={{color}}>
                {status.charAt(0).toUpperCase() + status.slice(1)}
            </Typography>
        </Box>
    );
};

export default StatusDisplay;