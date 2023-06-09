RankApp
RankApp is a web application built using ASP.NET Core and React. It allows users to rank movies and albums and add more movies and albums to rank. The application stores the ranking data in a database.

Getting Started
To run the RankApp project on your local machine, follow these steps:

Prerequisites
.NET 6 SDK: Ensure that you have the .NET 6 SDK installed on your machine.
Node.js: Install the latest LTS version of Node.js.
Installation
Clone the repository to your local machine.


Step 1: cd RankApp/RankApp
Step 2: dotnet restore
Step 3: dotnet build
Step 4: dotnet run

Open your web browser and navigate to the port displayed in the terminal (e.g., https://localhost:5001) to access the RankApp.

Configuration
The application uses a SQLite database by default. If you want to use a different database provider, you can modify the database connection string in the appsettings.json file.


"ConnectionStrings": {
  "DefaultConnection": "Data Source=rankapp.db"
}
You can also specify other application-specific settings in the appsettings.json file.

Usage
Rank Movies and Albums: Once the application is running, you can rank movies and albums by navigating to the respective pages and providing your ranking.
Add Movies and Albums: To add more movies and albums to rank, use the appropriate functionality provided in the application.
View Rankings: You can view the rankings of movies and albums on the corresponding pages.
Folder Structure
Controllers: Contains ASP.NET Core controllers responsible for handling HTTP requests and returning responses.
Models: Includes data models used by the application, which are typically persisted in the database.
Services: Contains the business logic or service classes for handling data access and processing.
ClientApp: Houses the React web application code.
src: Contains the React components, styles, and other related files.
public: Contains the public assets for the React app.


Contributing
If you want to contribute to RankApp, please follow the guidelines outlined in the CONTRIBUTING.md file.

License
This project is licensed under the MIT License.
