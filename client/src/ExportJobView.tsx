import {useExportJobContext} from './ExportJobContext';
import {Container, Typography, Button, Grid, Box} from '@mui/material';
import {ExportJobCard} from './Components/ExportJobCard';

function ExportJobView() {
    const {jobs, loading, error, refreshJobs, startJob} = useExportJobContext();

    if (loading)
        return <Typography variant="h6">Loading...</Typography>;
    if (error)
        return <Typography variant="h6" color="error">{error}</Typography>;

    return (
        <Container maxWidth="md" style={{marginTop: '2rem'}}>
            <Typography variant="h4" gutterBottom>
                Job List
            </Typography>
            <Box sx={{flexGrow: 1, gap: 2, display: 'flex'}}>
                <Button variant="contained" color="primary" onClick={refreshJobs} style={{marginBottom: '1rem'}}>
                    Refresh Jobs
                </Button>
                <Button variant="contained" color="secondary"
                        onClick={() => startJob('example-data-set')} // TODO: Drop down or something for data set
                        style={{marginBottom: '1rem'}}>
                    Start New Job
                </Button>
            </Box>
            <Grid container spacing={3}>
                {jobs?.map((job) => (
                    <Grid>
                        <ExportJobCard job={job}/>
                    </Grid>
                ))}
            </Grid>
        </Container>
    );
}

export default ExportJobView;
