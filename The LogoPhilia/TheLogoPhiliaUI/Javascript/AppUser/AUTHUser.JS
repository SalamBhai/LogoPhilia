var date = new Date();

var LoginInfo = localStorage.getItem('LoginInfo')
const StorageItem = JSON.parse(LoginInfo);
if(date.getTime() > StorageItem.expiry)
{
    localStorage.removeItem('LoginInfo');
    location.href = "/HTML/User/LoginPageUser.html";
}
const Token = StorageItem.token;

console.log(Token);

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