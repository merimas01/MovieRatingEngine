import { GoogleLogin } from "@react-oauth/google";
import { jwtDecode } from "jwt-decode";
import { useNavigate } from "react-router-dom";

export function Login() {

    const navigate = useNavigate();

    return (
        <>
            <div>
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
            </div>
        </>
    );
}
export default Login;