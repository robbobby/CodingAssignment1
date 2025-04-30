# Decisions / thought process... etc

## Assumptions

- I have assumed multiple users are able to access the same set of jobs through domain. Although I'm not entirely sure the code will indicate this.
-  I have assumed the backend is a single-instance (non-distributed) system.
   In a distributed environment, WebSocket connections would typically be managed by a dedicated microservice responsible for handling real-time communication.
   Instead of calling the SignalR Hub directly, the application would forward messages to this microservice, either via an internal API or a socket-based system, which would then relay messages to the appropriate clients.
- The spec states that the backend should perform polling to the third-party API.
  However, I initially thought that if the client authenticated directly and obtained a JWT (e.g., through a sign-in flow), it could securely poll the third-party API itself.
  In this assignment, I have followed the spec and implemented polling via the backend.
- The 3rd party API is assumed to only take one id at a time.

## Backend

- `Onion layer architecture` - api/db/service/core - ideally test project per layer
- `SignalR` - for frontend status tracking instead of polling. This is the better choice im my opinion. But not always the right choice.
 
### APIs
- /api/jobs
- Following RESTful principles
- `GET` - Get all jobs
- `POST /start` - Create a new job

### Notes

- Auth is out of spec, so commented where it would be
- Db layer/migrations are out of spec, so calls are made but no persistence

## Frontend

- `React` - `Mui`
- `Typescript`
- `React Context` for state management
- `SignalR` for polling updates