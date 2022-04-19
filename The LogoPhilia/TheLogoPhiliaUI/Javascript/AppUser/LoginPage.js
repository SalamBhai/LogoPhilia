console.log("Me Here")
const [emailAddress, userName, password, submitButton, loginForm,]= 
[document.getElementById('email'),document.getElementById('UserName'),document.getElementById('password'),
 document.getElementById('Login'), document.getElementById('loginForm')
]
const host = "https://localhost:5001"
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
            alert(`Login Successful ${results.name}`);
            localStorage.setItem('Token', results.token);
            location.href = "/HTML/User/UserProfile.html"
        })
        .catch(function(err) {
            console.error(err);
        })
})