const [signoutButton,  imageOnSidebar,NameOnSidebar, discussionCount,
     joinedYear,notes, imageCard,nameOnCard, country]
 = [document.getElementById('sign-out'),
 document.getElementById('img'), document.getElementById('Name'),
 document.getElementById('disc'), document.getElementById('year'),
document.getElementById('notes'), document.getElementById('imageCard'),
 document.getElementById('name'),document.getElementById('country') ]

signoutButton.addEventListener('click', function()
{
    localStorage.clear();
    location.href="/HTML/HOMEPAGE.HTML"
})
var date = new Date();

function GetLoginToken()
{
    var LoginInfo = localStorage.getItem('LoginInfo')
    console.log(LoginInfo);
    const StorageItem = JSON.parse(LoginInfo);
    if(date.getTime() > StorageItem.expiry)
    {
        localStorage.removeItem('LoginInfo');
        location.href = "/HTML/User/LoginPageUser.html";
        return null;
    }
    const userToken = StorageItem.token
    return userToken;
}

const userToken = GetLoginToken();
if(userToken === null )
{
    location.href = "/HTML/User/LoginPageUser.html";
}
const host='https://localhost:5001';
fetch('https://localhost:5001/api/ApplicationUser/GetLoggedInApplicationUser',{
    headers:{
      "Authorization": 'Bearer ' + userToken,
      "Content-Type": 'application/json'
    }
})
.then(function(response)
{
    return response.json();
})
.then(function(output)
{
    console.log(output);
     let srcurl= `https://localhost:5001/UserImages/${output.data.applicationUserImage}`;
    imageOnSidebar.setAttribute('src',srcurl);
    console.log(imageOnSidebar);
    NameOnSidebar.textContent = output.data.userName;
    imageCard.setAttribute('src',srcurl);
    nameOnCard.textContent= output.data.fullName;
    console.log(nameOnCard);
    const arrayDiscussion = Array.from(output.data.applicationUserPosts);
    discussionCount.textContent= arrayDiscussion.length;
    console.log(discussionCount);
    const notsArray = Array.from(output.data.applicationUserNotes);
    notes.textContent= notsArray.length;
    country.textContent = output.data.country;
    let year= output.data.dateOfCreation;
    console.log(year);
    var aYear= year.getFullYear();
    console.log(aYear);


})
.catch(function(err)
{
    console.error(err);
})