# top

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <h3 align="center">InboxAPI Project</h3>

  <p align="center">
    
  </p>
</div>


<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project


This is a back-end prototype project for accessing your inbox and interact with the inbox in various fun ways. i hope you enjoy it. 

* Only Authenticated users can interact with their inbox. 
* Users can send "message to each other".
* Users can delete and recover their messages.
* Users can create new labels or delete them.
* Users can add labels or remove them from their inboc messages.
* Users can view messages that has a specific label or from a specific email address.

<p align="right">(<a href="#top">back to top</a>)</p>

### Built With

This is the technologies and tools used in this project. 

* [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
* [Entity framework](https://docs.microsoft.com/en-us/ef/)
* [SQL](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15)
* [Draw.io](https://app.diagrams.net/)
* [Owin](https://docs.microsoft.com/en-us/aspnet/aspnet/overview/owin-and-katana/)
* [ASP.Net](https://dotnet.microsoft.com/apps/aspnet)
* [Git](https://git-scm.com/)
* [Postman](https://www.postman.com/)

<p align="right">(<a href="#top">back to top</a>)</p>

Not too hectic right? simplicity is the best. 

<!-- GETTING STARTED -->
## Getting Started

Here are the simple steps you will need to complete before we start having fun.

### Prerequisites

* SQL Server Management Studio Manager.
* Microsoft Visual Studio (preferrebly version 15 on wards)
* Setup folder include all the other files you will need. i.e _DatabaseStructure.sql_, _EmailServerDB.drawio_, _InboxAPI.postman_collection.json_, _InboxAPI.postman_environment.json_.

### Installation 

1. Create a local instance on your machine in SQL Server. you can call it "localhost". 

    * check out [Documentation](https://www.c-sharpcorner.com/blogs/creating-local-database-using-microsoft-sql-server) on how to create a local server instance running. 
    * Connect to the new instance via SSMS (SQL Server Management Studio).
    * Create a database and name it "_EmailServerDB_". Run "_DatabaseStructure.sql_" to create the necesary tables, make sure to connect to _EmailServerDB_.  

2. Clone the repo and open in 
   ```sh
   git clone https://github.com/VhonaniM2/InboxAPI.git
   ```
3. Install NugetPackages packages (it should let you restore them and install automatically. refer to packages.config file after cloning for packages you need). bellow is the command example on how to install via the Package Manager Console.
 ```js
   install-package entityframework -version 5.0.0.0
   ```
4. We are all set, now you can build the project. 

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- USAGE EXAMPLES -->
## Usage

Now we can start having fun with this prototype. We are going to use postman for all the testing and fun. Please refer to the postman collections for endpoints. 

* first register at least 2 accounts using the _RegisterUser_ end point. No Authentication Required here
* once you have registered, now you can _getToken_ and Authenticate for the rest of other endpoints. 

all Endpoints return the same reponse which is custom. below is an examplee from _getAllMessages_. _Data_ will contain the information requested.
```sh
{
    "Data": [
        {
            "Id": 2,
            "Message": "are you guys coming",
            "Deleted": false,
            "Label": "party",
            "DateReceived": "2021-10-10T20:02:01.793",
            "DateDeleted": null,
            "FromAddress": "test@test.com",
            "ToAddress": "test2@test.com"
        }
    ],
    "IsSuccessful": true,
    "Error": null
}
```

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- ROADMAP -->




<!-- CONTACT -->
## Contact

Vhonani Makhokha - [@your_twitter](https://twitter.com/your_username) - vhonani.tgb@gmail.com

Project Link: [https://github.com/VhonaniM2/InboxAPI](https://github.com/your_username/repo_name)

<p align="right">(<a href="#top">back to top</a>)</p>




