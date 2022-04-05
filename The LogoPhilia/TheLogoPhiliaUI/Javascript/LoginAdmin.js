
var userName = document.getElementById('UserName');
var password = document.getElementById('password')
var conpassword = document.getElementById('conpassword');
var loginForm = document.getElementById('loginForm');
var loginBtn = document.getElementById('Login');
var administratorCode = document.getElementById('adminCode');
var email = document.getElementById('email');
var homePageBody = document.getElementById('homePage');
const host = "https://localhost:5001"

loginForm.addEventListener('submit', function(e) {
    e.preventDefault();
    data = {
        "userName": userName.value,
        "password": password.value,
        "administratorCode": administratorCode.value,
        "email": email.value,
    };

    console.log("Processing Request...")
  
    fetch(`${host}/api/User/AdminLogin`, {
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
        console.log(results);
        alert(`Login Successful ${results.name}`);
        localStorage.setItem('Token', results.token);
        // let elementScript= document.createElement('script');
        // elementScript.setAttribute('src','/Javascript/Miscellaneous.js');
        // homePageBody.appendChild(elementScript);
        // console.log(elementScript);
        // console.log(homePageBody);
        location.href = "/HTML/HomePage.html";
         
    })
    .catch(function(err) {
        console.error(err);
    })
 
})