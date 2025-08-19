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
  const [filteredMovies, setFilteredMovies] = useState([]);
  const [filteredMoviesCurrentLength, setFilteredMoviesCurrentLength] = useState(0);
  const [filteredMoviesTotalLenght, setFilteredMoviesTotalLength] = useState(0);
  const [totalCountBeforeFilter, setTotalCountBeforeFilter] = useState(0);
  const [currentPage, setCurrentPage] = useState(0);
  const [pageSize, setPageSize] = useState(5);
  const [error, setError] = useState("");
  const [isChecked, setIsChecked] = useState(false);  //toggle switch (isShow)
  const [selectedMovie, setSelectedMovie] = useState(null);
  const [rateValue, setRateValue] = useState(null);
  const [notification, setNotification] = useState("");
  const [search, setSearch] = useState("");

  const fetchDefaultTop10 = async (isShow) => {
    setError("");
    setMovies([]);

    try {
      const basicAuth = btoa("test:test"); // username:password
      const response = await fetch(
        "http://localhost:5208/api/Movies/top10/" + isShow,
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

      setMovies(data);
      console.log(data);
    } catch (err) {
      setError(err.message);
    }
  };

  const fetchFilteredMovies = async (fts = "", isShow = false, page = 0, pageSize = 5) => {
    setError("");
    setPageSize(pageSize);

    console.log("fts: ", fts);
    console.log("is show: ", isShow);

    var url = `http://localhost:5208/api/Movies?FTS=${fts}&isShow=${isShow}&Page=${page}&PageSize=${pageSize}`
    try {
      const basicAuth = btoa("test:test"); // username:password
      const response = await fetch(
        url,
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

      console.log(data);

      if (page != 0) {
        const list3 = [...filteredMovies, ...(data.result)];
        setFilteredMovies(list3);
      }
      else
        setFilteredMovies(data.result);

      setFilteredMoviesCurrentLength(data.currentCount);
      setFilteredMoviesTotalLength(data.count);
      setTotalCountBeforeFilter(data.totalCountBeforeFilter);
    } catch (err) {
      setError(err.message);
    }
  };


  const handleSearchChange = async (e) => {
    const newValue = e.target.value;
    setSearch(newValue);
    setCurrentPage(0);

    console.log("new search value:", newValue.length);
    console.log("search", search.length);

    if (newValue.length >= 2 && newValue.trim() !== "") {
      console.log("isChecked", isChecked);
      fetchFilteredMovies(newValue.trim(), isChecked, 0, pageSize);
    }
    else{
      fetchDefaultTop10(isChecked);
      setFilteredMovies([]);
      console.log("filteredMovies when the search is 0", filteredMovies);
    }
  };

  useEffect(() => {
    setIsChecked(false);
    fetchDefaultTop10(false);
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

        //refresh the list
        search.length <= 1 ? fetchDefaultTop10(isChecked) : fetchFilteredMovies(search, isChecked, 0, pageSize)
      } catch (err) {
        setError(err.message);
      }
    }
  };

  // Function to run when switch changes
  const handleChange = async (event) => {
    setIsChecked(event.target.checked);
    console.log("search:", search.length);
    console.log("Switch is now:", event.target.checked);
    setCurrentPage(0); //kada se mijenja switch, treba se setovati i current page 
    if (search.length <= 1) {
      console.log("fetch default 10");
      fetchDefaultTop10(event.target.checked);
    }
    else fetchFilteredMovies(search, event.target.checked, 0, pageSize);
  };


  const showNotification = async (message) => {
    setNotification(message);

    // Automatically hide after 3 seconds
    setTimeout(() => {
      setNotification("");
    }, 3000);
  };

  return (
    <>
      <div style={{ padding: "2rem", fontFamily: "sans-serif" }}>

        <h1 className="title">Movie Rating Engine 🎬 </h1>

        <div
          className="search"
          style={{
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
            width: "100%",
            marginTop: "2rem",
          }}
        >
          <Box
            component="form"
            sx={{
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
              gap: 1, // spacing between input & button
              width: { xs: "90%", sm: "70%", md: "60%", lg: "50%" }, // responsive widths
              maxWidth: "1000px",
            }}
            noValidate
            autoComplete="off"
          >
            <TextField
              id="outlined-basic"
              label="Search..."
              variant="outlined"
              sx={{
                flex: 1,
                "& .MuiOutlinedInput-root": {
                  borderRadius: "50px",
                },
                "& .MuiOutlinedInput-notchedOutline": {
                  borderRadius: "50px",
                },
              }}
              value={search}
              onChange={(e) => handleSearchChange(e)}
            />

            <IconButton>
              <CloseIcon onClick={() => { setSearch(""); setFilteredMovies([]); setFilteredMoviesCurrentLength(0); setFilteredMoviesTotalLength(0); fetchDefaultTop10(isChecked) }} />
            </IconButton>
          </Box>
        </div>


        <div className="toggle">
          <Box display="flex" alignItems="center" justifyContent="space-between" width={260} >
            <Typography sx={{ fontSize: !isChecked ? "25px" : "15px", fontWeight: !isChecked ? "bold" : "normal" }}>Movies</Typography>
            <Switch checked={isChecked} onChange={handleChange} />
            <Typography sx={{ fontSize: isChecked ? "25px" : "15px", fontWeight: isChecked ? "bold" : "normal" }}>TV Shows</Typography>
          </Box>
        </div>

        <div>
          {search.length > 1 && search.trim()!="" && (filteredMoviesTotalLenght == 0) &&
            <div style={{
              marginBottom: "1rem", backgroundColor: "#f8d7da", color: "#721c24",
              padding: "10px",
            }}>Sadly, no matches found. </div>}

          <div className="grid-movies-shows" >
            {(search.length >= 2 && search.trim() != "" ? filteredMovies : movies).map((movie, index) => (

              <div className="movie-card"
                key={`${movie.movieId}-${index}`} onClick={() => setSelectedMovie(movie)}>
                <img
                  src={movie.coverImage
                    ? `data:image/jpeg;base64,${movie.coverImage}`
                    : "/assets/no-image.svg"}
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


          {/*ovo dugme se pojavi samo ako postoji paginacija, tj. vise od jedne stranice rezultata */}
          {search.length > 1 && search.trim() != "" && filteredMovies && filteredMoviesCurrentLength < filteredMoviesTotalLenght
            && <Box textAlign="center">
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
                onClick={() => { setCurrentPage(currentPage + 1); console.log(currentPage); fetchFilteredMovies(search, isChecked, currentPage + 1, pageSize); }}
              >View more results</Button>
            </Box>
          }

        </div>
        <div>
        </div>
      </div>


      {/* Rating functionality */}
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
            <IconButton onClick={() => { setSelectedMovie(""); setRateValue(null); }} sx={{
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

            <button className="btnRate" disabled={rateValue == null} onClick={() => {
              rateMovie(selectedMovie.movieId, rateValue); setSelectedMovie(""); setRateValue(null);

            }}>Rate</button>

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
