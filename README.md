# Semantic Holdings coding exercise

## Project Overview

This application consists of two main components: a backend developed with the .NET platform and a frontend created using the Angular framework. This guide provides step-by-step instructions for launching both parts.

## Dependencies Installation

Before starting, make sure you have the following tools installed:

- ASP.NET Core Runtime 9 to run backend part (https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- Node.js 20+ to run frontend part (https://nodejs.org/en/about/previous-releases)

## Running locally

Open two terminal windows in the root folder of the repository:

### Terminal #1 (Backend):

```bash
cd src/AccountingDashboard
dotnet run
```

Your backend server will now be listening on the configured port (by default it is http://localhost:5063).

### Terminal #2 (Frontend):

```bash
cd src/AccountingDashboardClientApp
npm install
npm run start
```

Now, you can access the frontend via http://localhost:4200 in your web browser.
