# TMS
---
title: Transportation Management System Developer Code Test
---

-   [Assumptions](#assumptions){#toc-assumptions}
-   [TODO (Including
    Assumptions)](#todo-including-assumptions){#toc-todo-including-assumptions}
-   [Questions ](#questions){#toc-questions}

# Assumptions

1\. Event Date Handling

-   Event Date:

    -   The EventDate in the uploaded CSV file represents the local date
        and time of the city where the event occurred.

    -   The EventDate is stored as a DateTimeOffset to capture the time
        zone information.

-   Time Zone Conversion:

    -   Each event's EventDate is converted to UTC using the time zone
        of the city where the event occurred.

    -   The converted UTC value is stored in the database.

-   City Time Zone:

    -   Each city has an associated time zone
        (e.g., America/New_York, Europe/London).

    -   The time zone is used to convert the local EventDate to UTC.

2\. Trip Duration Calculation

-   Trip Duration:

    -   The duration of a trip is calculated as the difference between
        the End_Date (UTC) and Start_Date (UTC).

    -   The duration is stored as TotalDurationInHours (a double value).

-   Display Format:

    -   The TotalDurationInHours is displayed in a user-friendly format
        (e.g., 2 hours 30 minutes).

3\. Event Processing Logic

-   Event Types:

    -   W: Starts a trip.

    -   A, D: Intermediate events assigned to the current trip.

    -   Z: Ends a trip.

-   Event Order:

    -   Events are processed in chronological order based on their
        UTC EventDate.

-   Edge Cases:

    -   If an A or D event occurs without a current trip, a new trip is
        started with HasIssue = true.

    -   If a W event occurs while a trip is already in progress, the
        current trip is closed, and a new trip is started.

    -   If a Z event occurs without a corresponding W, a trip is started
        and ended immediately with HasIssue = true.

4\. File Upload Logic

-   File Storage:

    -   Uploaded CSV files are stored in Azure Blob Storage.

    -   An Azure Function processes the file automatically when it is
        uploaded.

-   File Processing:

    -   The file is parsed, and events are validated and processed into
        trips.

    -   Invalid events (e.g., missing required fields) are logged and
        skipped.

5\. User Interface Logic

-   Trips List:

    -   Displays a list of trips with details
        like EquipmentId, Origin, Destination, Start_Date, End_Date,
        and TotalDurationInHours.

    -   Supports pagination for large datasets.

-   Trip Details:

    -   Displays detailed information about a specific trip, including
        its events.

    -   Includes a \"Back to Trips\" button for easy navigation.

6\. Error Handling

-   Validation Errors:

    -   Invalid CSV files or events are logged, and the user is
        notified.

-   API Errors:

    -   Failed API calls (e.g., fetching trips) are retried with
        exponential backoff.

-   Database Errors:

    -   Database connection issues are logged, and the user is notified.

7\. Security (TODO)

-   Authentication:

    -   Users authenticate using Azure Identity Platform (Azure AD).

-   Authorization:

    -   Role-based access control (RBAC) is implemented to restrict
        access to specific features (e.g., only admins can upload
        files).

-   Secrets Management:

    -   Sensitive information (e.g., database connection strings, API
        keys) is stored in Azure Key Vault.

8\. Scalability (TODO)

-   Database:

    -   SQLite is used for development. For production, use Azure SQL
        Database or SQL Server.

-   Caching:

    -   Frequently accessed data (e.g., trip lists) is cached
        using Azure Redis Cache.

-   Auto-Scaling:

    -   The application is hosted on Azure App Service or AKS with
        auto-scaling enabled.

# TODO (Including Assumptions)

Here are some TODO comments to highlight areas for future improvement
based on the updated assumptions:

// TODO: Implement time zone conversion for EventDate (local to UTC) in
TMS.Application.

// TODO: Store EventDate as DateTimeOffset in the database to capture
time zone information.

// TODO: Calculate TotalDurationInHours as the difference between
End_Date (UTC) and Start_Date (UTC) in TMS.Application.

// TODO: Format TotalDurationInHours as hours and minutes (e.g., \"2
hours 30 minutes\") in TMS.App.

// TODO: Implement trip logic (start with W, end with Z, handle A/D
events) in TMS.Application.

// TODO: Add validation for uploaded CSV files (e.g., check for required
columns) in TMS.Application.

// TODO: Implement edge cases (e.g., A/D without W, Z without W) in
TMS.Application.

// TODO: Store uploaded CSV files in Azure Blob Storage (create
AzureBlobStorageService in TMS.infra).

// TODO: Create an Azure Function (TMS.Functions) to process files
automatically when uploaded to Blob Storage.

// TODO: Implement pagination for the trips list in TMS.App.

// TODO: Add a \"Back to Trips\" button in the TripDetail page in
TMS.App.

// TODO: Implement Azure Identity Platform authentication and role-based
access control in TMS.host.

// TODO: Store secrets (e.g., connection strings, API keys) in Azure Key
Vault and retrieve them in TMS.host.

// TODO: Set up Azure Application Insights for monitoring and logging in
TMS.host and TMS.App.

// TODO: Implement caching using Azure Redis Cache for frequently
accessed data in TMS.host.

// TODO: Set up a CI/CD pipeline using Azure DevOps or GitHub Actions
for TMS.host and TMS.App.

// TODO: Migrate from SQLite to Azure SQL Database for production in
TMS.infra.

// TODO: Implement auto-scaling for the application using Azure App
Service or AKS.

// TODO: Use Azure Service Bus or Event Grid for event-driven
architecture in TMS.host.

// TODO: Add unit and integration tests for all components in
TMS.domain, TMS.Application, TMS.infra, and TMS.host.

// TODO: Implement OWASP security best practices (e.g., HTTPS, input
validation) in TMS.host.

# Questions 

1.  Time Zone Data:

    -   Where is the time zone data for each city stored? Is it in the
        database or fetched from an external API?

2.  Event Date Validation:

    -   Should we validate that the EventDate in the CSV file is in the
        correct format and falls within a reasonable range?

3.  Time Zone Conversion Library:

    -   Which library or method will be used for time zone conversion
        (e.g., TimeZoneInfo in .NET)?

4.  UTC Storage:

    -   Should we store both the local EventDate and the
        UTC EventDate in the database, or just the UTC value?

5.  Time Zone Changes:

    -   How will we handle cities that change their time zones (e.g.,
        due to daylight saving time)?

6.  Data Volume:

    -   What is the expected volume of trips and events? This will
        determine the need for pagination, batch processing, and
        caching.

7.  User Roles:

    -   Are there different user roles (e.g., admin, viewer)? If so, how
        should access control be implemented?

8.  Deployment Environment:

    -   Where will the application be deployed (e.g., cloud,
        on-premises)? This affects scalability and performance
        considerations.

9.  Reporting Requirements:

    -   Are there any reporting requirements (e.g., generating trip
        summaries or analytics)?

10. Third-Party Integrations:

    -   Will the application need to integrate with other systems (e.g.,
        external APIs, logging services)?

11. Budget:

    -   What is the budget for Azure resources (e.g., Blob Storage, Key
        Vault, App Service)?

12. Compliance:

    -   Are there any compliance requirements (e.g., GDPR, HIPAA)?
