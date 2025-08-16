import React, { useEffect, useState, useRef } from "react";
import "./HomePageStyle.css";
import { Switch, Box } from "@mui/material";
import Button from '@mui/material/Button';
import Rating, { ratingClasses } from '@mui/material/Rating';
import Typography from '@mui/material/Typography';
import TextField from '@mui/material/TextField';
import CloseIcon from '@mui/icons-material/Close';
import IconButton from '@mui/material/IconButton';


const HomePage = () => {

  const [movies, setMovies] = useState([]);
  const [error, setError] = useState("");
  const [isChecked, setIsChecked] = useState(false);  //toggle switch (isShow)
  const [labelToggle, setLabelToggle] = useState("Movies");
  const [take, setTake] = useState(10);
  const [skip, setSkip] = useState(0);
  const [selectedMovie, setSelectedMovie] = useState(null);
  const [rateValue, setRateValue] = useState(null);
  const [notification, setNotification] = useState("");

  const fetchMovies = async (_isShow, _take, _skip) => {
    setError("");
    setMovies([]);
    setTake(_take);
    setSkip(_skip);

    try {
      const basicAuth = btoa("test:test"); // username:password
      const response = await fetch(
        "http://localhost:5208/api/Movies/next10/" + _isShow + "/" + _take + "/" + _skip,
        {
          method: "GET",
          headers: {
            "Authorization": `Basic ${basicAuth}`,
            "Accept": "application/json",
          },
        }
      );

      if (!response.ok) {
        throw new Error(`Error ${response.status}: ${response.statusText}`);
      }

      const data = await response.json();

      // console.log(movies);
      // const combined = [...movies, ...data];
      // console.log(combined); // [1, 2, 3, 4, 5, 6]

      setMovies(data);
      console.log(data);
    } catch (err) {
      setError(err.message);
    }
  };

  useEffect(() => {
    setIsChecked(false);
    setLabelToggle("Movies");
    fetchMovies(false, 10, 0);
  }, []);


  const rateMovie = async (movieId, rateValue) => {
    console.log("rateValue", rateValue);
    if (rateValue != null) {
      try {
        const basicAuth = btoa("test:test"); // username:password
        const response = await fetch(
          "http://localhost:5208/api/MovieRatings/",
          {
            method: "POST",
            headers: {
              "Authorization": `Basic ${basicAuth}`,
              "Accept": "application/json",
              "Content-Type": "application/json", 
            },
            body: JSON.stringify({ movieId: movieId, rate: rateValue }),
          }
        );

        if (!response.ok) {
          throw new Error(`Error ${response.status}: ${response.statusText}`);
        }

        const data = await response.json();
        console.log("Response:", data);

        showNotification("Rated successfully!");
      } catch (err) {
        setError(err.message);
      }
    }
  };

  // Function to run when switch changes
  const handleChange = (event) => {
    setIsChecked(event.target.checked);
    console.log("Switch is now:", event.target.checked);
    event.target.checked == true ? setLabelToggle("Shows") : setLabelToggle("Movies");
    fetchMovies(event.target.checked, take, skip);

  };


  const showNotification = (message) => {
    setNotification(message);

    // Automatically hide after 3 seconds
    setTimeout(() => {
      setNotification("");
    }, 3000);
  };

  return (
    <>
      <div style={{ padding: "2rem", fontFamily: "sans-serif" }}>

        <h1 className="title">Movie Rating Engine</h1>

        <div className="search-toggle">


          <Box
            component="form"
            sx={{ '& > :not(style)': { m: 0, width: '120ch' } }}
            noValidate
            autoComplete="off"
          >

            <TextField id="standard-basic" label="🔍 Search..." variant="standard" />
          </Box>

          <Box display="flex" alignItems="center" justifyContent="space-between" >
            <Typography sx={{ fontSize: !isChecked ? "25px" : "15px", fontWeight: !isChecked ? "bold" : "normal" }}>Movies</Typography>
            <Switch checked={isChecked} onChange={handleChange} />
            <Typography sx={{ fontSize: isChecked ? "25px" : "15px", fontWeight: isChecked ? "bold" : "normal" }}>Shows</Typography>
          </Box>

        </div>


        <div className="grid-movies-shows">
          {movies.map((movie) => (

            <div className="movie-card"
              key={movie.movieId} onClick={() => setSelectedMovie(movie)}>
              <img
                src={`data:image/jpeg;base64,${movie.coverImage}`}
                alt={movie.title}
                style={{ width: "100%", height: "300px", objectFit: "cover" }}
              />
              <div style={{ padding: "10px" }}>
                <h3 style={{ margin: "0 0 5px 0" }}>{movie.title}</h3>
                <p style={{ margin: "0 0 5px 0", fontSize: "0.9rem" }}>
                  {movie.description}
                </p>
                <p style={{ margin: 0 }}>⭐ {movie.averageRate}</p>

              </div>

            </div>

          ))}

        </div>


        <Box textAlign="center">
          <Button variant="outlined" sx={{
            borderColor: "primary.main",
            color: "primary.main",
            margin: "20px",
            "&:hover": {
              borderColor: "primary.main",
              backgroundColor: "primary.main",
              color: "white",
            },

          }}
          >View more results</Button>
        </Box>
        <div>
        </div>
      </div>



      {selectedMovie && (
        <div
          style={{
            position: "fixed",
            top: 0,
            left: 0,
            width: "100%",
            height: "100%",
            backgroundColor: "rgba(0,0,0,0.5)",
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
            padding: "10px"
          }}
        >
          <div
            style={{
              position: "relative",
              background: "white",
              padding: "20px",
              borderRadius: "8px",
              minWidth: "200px",
              display: "flex",
              flexDirection: "column", 
              gap: "5px", 
              alignItems: "center",
              width: "400px",
              maxWidth: "90%"

            }}
          >
            <IconButton onClick={() => setSelectedMovie("")} sx={{
              position: "absolute",
              top: "5px",
              right: "5px",
              border: "none",
              background: "transparent",
              fontSize: "16px",
              cursor: "pointer",
              color: "grey"
            }}>
              <CloseIcon />
            </IconButton>


            <div style={{ display: "flex", gap: "5px", marginTop: "20px" }}>
              <p>Rate: </p> <p style={{ fontWeight: "bold" }}>{selectedMovie.title}</p>
            </div>

            <Typography component="legend"></Typography>
            <Rating
              name="simple-uncontrolled"
              onChange={(event, newValue) => {
                console.log(newValue);
                setRateValue(newValue);
              }}
              defaultValue={0}
            />

            <button className="btnRate" disabled={rateValue == null} onClick={() => { rateMovie(selectedMovie.movieId, rateValue); setSelectedMovie(""); setRateValue(null); }}>Rate</button>

          </div>
        </div>
      )}


      {notification && (
        <div
          style={{
            position: "fixed",
            bottom: "20px",
            left: "50%",
            transform: "translateX(-50%)",
            backgroundColor: "#28a745",
            border: "1px solid #28a745",
            color: "white",
            padding: "10px 20px",
            borderRadius: "8px",

            //boxShadow: "0 2px 6px rgba(0,0,0,0.3)",
          }}
        >
          {notification}
        </div>
      )}
    </>

  );
};

export default HomePage;
