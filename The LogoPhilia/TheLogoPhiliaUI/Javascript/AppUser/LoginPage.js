console.log("Me Here")
const [emailAddress, userName, password, submitButton, loginForm,]= 
[document.getElementById('email'),document.getElementById('UserName'),document.getElementById('password'),
 document.getElementById('Login'), document.getElementById('loginForm')
]


function EffectReLogin()
{
    localStorage.removeItem("Token");
    location.href = "/HTML/User/UserProfile.html";
}
const host = "https://localhost:5001";

loginForm.addEventListener('submit', function(e) {
    
    e.preventDefault();

    submitButton.setAttribute('disabled', true);
   submitButton.style.cursor="not-allowed";

    console.log("Me Here");

    console.log(emailAddress.value)
    data = {
        "emailAddress": emailAddress.value,
        "userName": userName.value,
        "password": password.value,
    };
    
    console.log("Processing Request...")
     const date = new Date();
    fetch(`${host}/api/User/UserLogin`, {
            method: "POST",
            body: JSON.stringify(data),
            headers: {
                "Content-Type": "Application/json",
            }
        })
        .then(function(output) {
            console.log("Happening..");
            return output.json();
        })
        .then(function(results) {
            const expireTime = 10800000;
            alert(`Login Successful ${results.name}`);
            localStorage.setItem('Token', results.token)
            localStorage.setItem('userName',results.name)
            const LoginInfo = {
               token: results.token,
               userName: results.name,
               expiry: date.getTime() + expireTime,
            }
            localStorage.setItem('LoginInfo', JSON.stringify(LoginInfo));
            location.href = "/HTML/User/UserProfile.html";
        })
        .catch(function(err) {
            console.error(err);
        })
})