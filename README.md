# UP_PowerPlant
Simple application (PoC) for monitoring and managing solar power plant. 
Consists of: 
- basic CRUD for Power Plants
- fetching actual Power Production data
- fetching forecast Power Production data based on the weather forecast fecthed from the public API [Visual Crossing](https://www.visualcrossing.com/resources/documentation/weather-api/timeline-weather-api)
- JWT Authentication & Authorization
- Logging
- Swagger
- Postman collection and environment

<h2>Architecture:</h2>
Clean Architecture with DDD

## Table of contents  
1. [Prerequisties](#prerequisites)
2. [Installation](#installation) 
3. [Basic usage](#basicUsage)
4. [Postman collection](#postman)
5. [Swagger](#swagger)
6. [Get-timeseries endpoint details](#timeseries-endpoint)

<a name="prerequisites"></a>
## Prerequisties
```
Installed locally:
.Net 8
SQL Server
```

<a name="installation"></a>
## Installation
- you have to have default SQL Server instance called "MSSQLSERVER"
- if not, replace the "." on the connection string found in appsettings.json with your own instance name

<a name="basicUsage"></a>
## Basic usage  
- Start the app (the DB should automatically seed)
- Import Postman collection and environment
- Login with: ``` {
    "email": "admin@admin.com",
    "password": "admin"
}``` 
(_if you login with Postman, token will automatically be set inside headers for all the requests_)
- Or Register new user via Swagger or Postman

<a name="postman"></a>
## Postman collection
![image](https://github.com/user-attachments/assets/4d377bd4-3699-43ce-a5db-f08bca238c8e)

<a name="swagger"></a>
## Swagger
/swagger
![image](https://github.com/user-attachments/assets/d44ef2b4-d47a-4bc9-95da-65e638f61f3e)

<a name="timeseries-endpoint"></a>
## Get-timeseries endpoint details
- TimeseriesType: 0 - RealProduction ; 1 - ForecastedProduction
- TimeseriesGranularity: 0 - FifteenMinutes ; 1 - OneHour
- TimeseriesType ForecastedProduction returns the same result for each TimeseriesGranularity 
because of the external API limitations
