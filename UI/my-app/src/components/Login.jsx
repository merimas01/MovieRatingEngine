import { useEffect, useState } from "react";
import "./LoginPageStyle.css";
import { Switch, Box } from "@mui/material";
import Button from '@mui/material/Button';
import Rating, { ratingClasses } from '@mui/material/Rating';
import Typography from '@mui/material/Typography';
import TextField from '@mui/material/TextField';
import CloseIcon from '@mui/icons-material/Close';
import IconButton from '@mui/material/IconButton';
import LoginIcon from "@mui/icons-material/Login";
import { useNavigate } from "react-router-dom";



const handleAlert = async (isAlert, message) => {
    setShowAlert(isAlert);
    setMessage(message);
};

const Login = () => {

    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();

    const [showAlert, setShowAlert] = useState(false);
    const [alertMessage, setMessage] = useState("");



    const login = async (username, password) => {
        if (!username || !password) {
            setShowAlert(true);
            setMessage("Username and password required!");
            //alert("Username and password required!");
            return;
        }

        try {
            const basicAuth = btoa(username + ":" + password);
            const response = await fetch("http://localhost:5208/api/Users", {
                method: "GET",
                headers: {
                    "Authorization": `Basic ${basicAuth}`,
                    "Accept": "application/json",
                    "Content-Type": "application/json",
                },
            });

            if (!response.ok) {
                setShowAlert(true);
                setMessage("Login unsuccessful!");
                //alert("Login unsuccessful!");
                return;
            }

            let data;
            try {
                data = await response.json();
            } catch {
                setShowAlert(true);
                setMessage("Invalid server response.");
                //alert("Invalid server response.");
                return;
            }

            const user = data.result.find(u => u.username === username);

            if (!user) {
                setShowAlert(true);
                setMessage("Invalid username or password.");
                //   alert("Invalid username or password.");
                return;
            }

            const userFullName = `${user.firstName} ${user.lastName}`;
            const userId = user.id;

            navigate("/", { state: { userFullName, userId } });

        } catch (err) {
            console.error("Login error:", err);
            // alert("Something went wrong. Please try again later.");
            setShowAlert(true);
            setMessage("Something went wrong. Please try again later.");
        }
    };


    const handleSearchChangeUsername = async (e) => {
        const newValue = e.target.value;
        setUsername(newValue);
    };
    const handleSearchChangePassword = async (e) => {
        const newValue = e.target.value;
        setPassword(newValue);
    };


    return (<>
        <div className="Login">
            <h2 style={{ marginBottom: "20px" }}> LOGIN </h2>
            <Box
                component="form"
                sx={{
                    display: "flex",
                    alignItems: "center",
                    justifyContent: "center",
                    flexDirection: "column",
                    gap: 1,
                    width: { xs: "90%", sm: "70%", md: "60%", lg: "50%" }, // responsive widths
                    maxWidth: "1000px",
                }}
                autoComplete="off"
            >
                <TextField
                    id="outlined-basic"
                    label="Username"
                    variant="outlined"
                    required
                    sx={{
                        flex: 1,
                        "& .MuiOutlinedInput-root": {
                            borderRadius: "50px",
                        },
                        "& .MuiOutlinedInput-notchedOutline": {
                            borderRadius: "50px",
                        },
                        maxWidth: "300px",
                        width: "100%"
                    }}
                    value={username}
                    onChange={(e) => handleSearchChangeUsername(e)}
                    error={username === ""}
                    helperText={username === "" ? "Username is required" : ""}
                />

                <TextField
                    id="outlined-basic2"
                    label="Password"
                    variant="outlined"
                    type="password"
                    required
                    sx={{
                        flex: 1,
                        "& .MuiOutlinedInput-root": {
                            borderRadius: "50px",
                        },
                        "& .MuiOutlinedInput-notchedOutline": {
                            borderRadius: "50px",
                        },
                        maxWidth: "300px",
                        width: "100%"
                    }}
                    value={password}
                    onChange={(e) => handleSearchChangePassword(e)}
                    error={password === ""}
                    helperText={password === "" ? "Password is required" : ""}
                />
            </Box>


            <Button sx={{ marginTop: "20px" }}
                variant="contained"
                color="primary"
                startIcon={<LoginIcon />}
                onClick={() => { login(username, password) }}
            >
                Login
            </Button>
        </div>

        <div>

            {showAlert && (
                <div style={{
                    padding: "10px",
                    backgroundColor: "#f8d7da",
                    marginTop: "10px",
                    borderRadius: "5px",
                    textAlign: "center"
                }}>
                    {alertMessage}
                    <button onClick={() => { setShowAlert(false); setMessage(""); setUsername(""); setPassword(""); }}
                        style={{ marginLeft: "10px", border: "1px", padding: "10px", borderRadius: "10px" }}>Ok</button>
                </div>
            )}
        </div>

    </>);
};
export default Login;