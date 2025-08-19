import { Google, Height } from "@mui/icons-material";
import { GoogleLogin } from "@react-oauth/google";
import { jwtDecode } from "jwt-decode";
import { useNavigate } from "react-router-dom";

export function Login() {

    const navigate = useNavigate();

    return (
        <>
            <div
                style={{
                    display: "flex",
                    justifyContent: "space-evenly",
                    alignItems: "center",
                    height: "100vh", // full screen height
                }}
            >
                <GoogleLogin
                    onSuccess={(credentialResponse) => {
                        console.log(credentialResponse);
                        const user = jwtDecode(credentialResponse.credential);
                        console.log("Token (user info): ", user);
                        const uniqueUserInfo = user.sub;
                        navigate("/home/" + uniqueUserInfo);
                    }}
                    onError={() => console.log("Login failed.")}
                    auto_select={true}
                />


                <button style={{border:"1px", borderRadius:"10px", padding:"10px"}} onClick={()=>{navigate("/home")}}>Go to home page without authorization</button>
            </div>      
        </>
    );
}
export default Login;