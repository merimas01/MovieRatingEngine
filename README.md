# Movie Rating Engine

This is a web app where users can search through movies/TV shows and rate them using stars.
Built with .NET, ReactJs and SQL Server.

## Main functionalities:

- Toggle view
Users can toggle between Top 10 movies and Top 10 TV shows sorted by their average rate.

- Intelligent and automatic search engine
This engine automatically activates after 2 characters are entered. It matches any textual atrribute of the movie or TV show (title, description, cast) and can intelligently parse phrases such as: "5 stars", "at least 3 stars", "after 2016", "older than 5 years" etc. Search results are also sorted by rating. 

- "No matches" message
Users are informed if no matches are found. Below the message they can see all the movies/TV shows.

- Pagination with a button "View more results"
The button loads the next 5 items when clicked.

- Star rating
Users can annonymously rate movies/TV shows from 1 to 5 stars. The user gets a notification with a success message and the average rate of the movie/TV show automatically updates. Button "Rate" is enabled only when user chooses the star.

- Responsive UI
Responsive behaviour on different screen sizes.
