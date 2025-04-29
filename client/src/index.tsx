import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import ExportJobView from './ExportJobView';
import {ExportJobProvider} from "./ExportJobContext";

const root = ReactDOM.createRoot(
    document.getElementById('root') as HTMLElement
);
root.render(
    <React.StrictMode>
        <ExportJobProvider>
            <ExportJobView/>
        </ExportJobProvider>
    </React.StrictMode>
);
