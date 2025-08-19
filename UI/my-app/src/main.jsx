import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";
import { BrowserRouter } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.css";
import { GoogleOAuthProvider } from "@react-oauth/google";


const CLIENT_ID = import.meta.env.VITE_GOOGLE_CLIENT_ID;

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <BrowserRouter>
    <GoogleOAuthProvider clientId={CLIENT_ID}>
      <App />
      </GoogleOAuthProvider>
    </BrowserRouter>
  </React.StrictMode>
);
