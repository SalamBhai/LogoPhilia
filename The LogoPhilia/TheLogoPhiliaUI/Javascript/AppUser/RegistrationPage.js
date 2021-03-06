var registerForm = document.getElementById('registerForm');
var GenderSelect = document.getElementById('Gender');
var male = document.getElementById('male');
var female = document.getElementById('female');
var registerButton = document.getElementById('submit');

console.log(registerForm);
const host = 'https://localhost:5001';
fetch(`${host}/api/Enums/GetGenders`)
    .then(function(response) {
        return response.json();
    }).then(function(result) {
        console.log(result)
        male.innerHTML = result[0];
        GenderSelect.appendChild(male);

        female.textContent = result[1];
        GenderSelect.appendChild(female);

    }).catch(function(err) {
        console.error(err)
    })

    function ValidateInputs(){
        registerButton.addEventListener('click',function()
         {
          while(password.value!==confirmPassword.value)
          {
              alert('Passwords Do Not Match');
              if (password.value === confirmPassword.value) {
                   break;
              }
          }
             if (password.value === userName.value) {
             alert("Provide a password different from your userName");
            }
         })
     }
 
registerForm.addEventListener('submit', function(e) {
    e.preventDefault();
        ValidateInputs();
   
    var password = document.getElementById('password');
    var confirmPassword = document.getElementById('confirmpassword');
    if (password.value !== confirmPassword.value) {
        alert("Passwords Do Not Match");
    }
    if (password.value === userName.value) {
        alert("Provide a password different from your userName");
    }

    let formData = new FormData(registerForm);
    

    console.log('about executing...');
    console.log('fd', formData)
    fetch(`${host}/api/ApplicationUser/CreateApplicationUser`, {
            method: "POST",
            body: formData,
        })
        .then(function(response) {
            return response.json();
        }).then(function(result) {
            if(result.success===false)
            {
                alert(`Registration Unsuccessful User With ${userName.value} Already Exists`)
            }
            if (result.data!== null) {
                console.log(result);
                alert(`Registration Successful!  ${result.data.fullName}`);
                location.href = "/HTML/LoginPageUser.html";
            }
        })
        .catch(function(err) {
            alert(err);
        })

})