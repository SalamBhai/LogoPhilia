var emailAddress = document.getElementById('email');
var userName = document.getElementById('UserName');
var password = document.getElementById('password')
var conpassword = document.getElementById('conpassword');
var loginForm = document.getElementById('loginForm');
const host = "https://localhost:5001"
loginForm.addEventListener('submit', function(e) {
    e.preventDefault();

    data = {
        "emailAddress": emailAddress.value,
        "userName": userName.value,
        "password": password.value,
    };
    if (conpassword.value !== password.value) {
        alert('Passwords Do Not Match');
    }
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
            location.href = "/HTML/UserProfile.html"
        })
        .catch(function(err) {
            console.error(err);
        })
})