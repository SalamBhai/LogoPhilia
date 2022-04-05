const [signoutButton, token, imageOnSidebar,Name, discussionCount, joinedYear,notes ]
 = [document.getElementById('sign-out'), localStorage.getItem('Token'),document.getElementById('img'), document.getElementById('Name')
,document.getElementById('disc'),document.getElementById('year'),document.getElementById('notes')]
signoutButton.addEventListener('click',function()
{
    localStorage.clear();
    location.href="/HTML/HOMEPAGE.HTML"
})
const host='https://localhost:5001';
fetch(`${host}/api/ApplicationUser/GetLoggedInApplicationUser`,
{
   method:"GET",
   headers:{
       "Authorization": "Bearer" + token,
   }
}).then(function(response)
{
    return response.json();
}).then(function(final)
{
   console.log('ff',final);
})
.catch(function(err)
{
    console.error(err);
})

