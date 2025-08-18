# Movie Rating Engine

This is a web app where users can search through movies/TV shows and rate them using stars.
Built with .NET, ReactJs and SQL Server.

## Main characteristics:

### Toggle view
- Users can toggle between Top 10 movies and Top 10 TV shows sorted by their average rate.
<img width="1892" height="907" alt="toggle" src="https://github.com/user-attachments/assets/3f4b0df9-5f64-4ac3-9eb8-e49b8641bb01" />

### Intelligent and automatic search engine
- This engine automatically activates after 2 characters are entered. It matches any textual atrribute of the movie or TV show (title, description, cast) and can intelligently parse phrases such as: "5 stars", "at least 3 stars", "after 2016", "older than 5 years" etc. Search results are also sorted by rating. 
<img width="1892" height="902" alt="search" src="https://github.com/user-attachments/assets/3cf5097c-8f91-4f69-9430-48a7cc4abec5" />

- "No matches" message - Users are informed if no matches are found. Below the message they can see all the movies/TV shows.
<img width="1895" height="906" alt="nomatches" src="https://github.com/user-attachments/assets/fbbe45b5-b2a2-416f-a689-90086e20db13" />

### Pagination with a button "View more results"
- The button loads the next 5 items when clicked.
<img width="1900" height="900" alt="btnView" src="https://github.com/user-attachments/assets/5c908ba2-8702-42ef-b68b-202b0b90e290" />

### Star rating
- Users can annonymously rate movies/TV shows from 1 to 5 stars. The user gets a notification with a success message and the average rate of the movie/TV show automatically updates. Button "Rate" is enabled only when user chooses the star.
<img width="1877" height="905" alt="rate" src="https://github.com/user-attachments/assets/ba5773b6-8404-4390-af5d-c45f98e56094" />

### Responsive UI
- Responsive behaviour on different screen sizes.
<img width="621" height="868" alt="responsive" src="https://github.com/user-attachments/assets/e66e0d34-c65d-4d52-8525-a0a58fa57ac6" />


## Setup instructions

### Prerequisites
- Visual Studio (ASP .NET Core framework v8)
- Visual Studio Code
- Node.js (v22.14.0)
- Set your OpenAI API Key in Environment variable called ***API_KEY*** and restart your PC

### Backend
- Open **MRE** project in Visual Studio 2022
- Change the connection string in *appsettings.json* with your data 
- Build -> Rebuild Solution
- Debug -> Start without debugging (run as http)
- The app should open at: http://localhost:5208/swagger/index.html 
- Swagger authorization: username - *test* & password - *test*

### Frontend
- Open **my-app** project in Visual Studio Code
- Open terminal in my-app folder (Command prompt)
- Write these commands:
- - npm install
- - npm run dev
- The app should open at: http://localhost:5173/
