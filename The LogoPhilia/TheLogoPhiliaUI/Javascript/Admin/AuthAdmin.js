let adminToken= localStorage.getItem('AdminToken');
if(adminToken===undefined)
{
    location.href="/HTML/LoginPageAdmin.html";
}