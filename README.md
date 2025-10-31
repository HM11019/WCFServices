⚙️ WCF Services – Northwind Data Provider

This project implements a Windows Communication Foundation (WCF) service layer designed to expose business data and operations from the Northwind database.
Each service retrieves and processes data using Entity Framework and LINQ, providing structured results that can be consumed by external client applications — such as the ASP.NET Core MVC UI client.

🧩 Overview

The WCF Services project serves as the data access and business logic layer for the application ecosystem.
It allows remote systems to query information from the Northwind database via REST-style endpoints, returning data in JSON format.

Each service operation uses Entity Framework to query the database and LINQ expressions to efficiently filter, sort, and project data into lightweight DTOs.

⚙️ Core Features

✅ Entity Framework ORM — full data model generated from Northwind database
✅ LINQ Queries — strongly typed data retrieval with filtering and sorting
✅ WCF Endpoints — expose services as RESTful APIs accessible via HTTP
✅ JSON Serialization — output formatted for easy client consumption
✅ Logging (WebTrack) — every client action is logged for auditing and analytics
✅ Dependency Injection Ready — supports injection for EF contexts and services

🧠 Data Access Layer

The Northwind database was fully mapped using Entity Framework Designer.


🧾 WebTrack Log Action

Each time a client consumes a WCF endpoint, an entry is recorded in the WebTrack table.
This ensures traceability of all API calls, allowing administrators to monitor usage and audit activities.

The following data is logged for every request:

Field	Description
URLRequest	Full request URL accessed by the client
SourceIp	IP address of the requesting client
TimeOfAction	Timestamp when the action occurred


🌐 Example Endpoint

CustomerService.svc/GetCustomersByCountry?country=Mexico

Request

GET http://localhost:54812/CustomerService.svc/GetCustomersByCountry?country=Mexico


Response

[
  {
    "CustomerID": "ANATR",
    "CompanyName": "Ana Trujillo Emparedados y helados",
    "ContactName": "Ana Trujillo",
    "Phone": "(5) 555-4729",
    "Fax": "(5) 555-3745"
  },
  {
    "CustomerID": "ANTON",
    "CompanyName": "Antonio Moreno Taquería",
    "ContactName": "Antonio Moreno",
    "Phone": "(5) 555-3932",
    "Fax": null
  }
]

🧩 Technologies Used

Layer	Technology
Framework	.NET Framework 4.8
Data Access	Entity Framework 6 (Database First – Northwind)
Language	C#
Communication	WCF (HTTP + JSON)
Logging	Entity-based table WebTrack
IDE	Visual Studio 2019 / 2022
⚙️ Configuration

Database Connection

Ensure the connectionStrings section in Web.config points to your Northwind

🧩 Technologies Used

Layer	Technology
Framework	.NET Framework 4.8
Data Access	Entity Framework 6 (Database First – Northwind)
Language	C#
Communication	WCF (HTTP + JSON)
Logging	Entity-based table WebTrack
IDE	Visual Studio 2019 / 2022
⚙️ Configuration

Database Connection

Ensure the connectionStrings section in Web.config points to your Northwind database:

<connectionStrings>
    <add name="NorthwindEntities"
         connectionString="metadata=res://*/Models.NorthwindModel.csdl|res://*/Models.NorthwindModel.ssdl|res://*/Models.NorthwindModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=.\SQLEXPRESS;initial catalog=Northwind;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;"
         providerName="System.Data.EntityClient" />
</connectionStrings>


Enable Metadata Publishing (if not enabled):

<serviceMetadata httpGetEnabled="true" httpsGetEnabled="true" />


Start the WCF Service

Run the project (F5) or host in IIS Express.
Default URL example:

http://localhost:54812/CustomerService.svc

🔗 Integration with Client Application

This WCF project is designed to work seamlessly with the companion ASP.NET Core MVC Client.
The client connects to these endpoints using HTTP requests and displays the results using Telerik Kendo UI Grids.

Example client configuration (appsettings.json):

"EndpointOption": {
  "ServicesBaseUrl": "http://localhost:54812/",
  "AllCustomers": "CustomerService.svc/GetCustomersByCountry?country="
}

🧩 Future Improvements

Implement additional services for Orders, Products, and Regions

Add exception logging to the WebTrack table

Include user authentication and role-based access

Add async EF operations for improved scalability

📜 License

This project is provided for educational and demonstration purposes.
All rights to the Northwind database schema and sample data belong to Microsoft.
