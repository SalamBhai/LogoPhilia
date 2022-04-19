let outResponse;
const [titleInput, textArea, submitButton, form]= [
document.querySelector('#title-Input'), document.querySelector('#postContent'), document.querySelector('#submitButton'),document.querySelector('#form')]


form.addEventListener('submit', async function(e)
{
    e.preventDefault();
   console.log(textArea.value);
   submitButton.setAttribute('disabled', true);
   submitButton.style.cursor="not-allowed";
    //    await FilterPostContent();
    SendRequestToMyServer();
    console.log(outResponse);

})
 async function FilterPostContent()
{
   
    const options = {
        method: 'GET',
        headers: {
            'X-RapidAPI-Host': 'community-purgomalum.p.rapidapi.com',
            'X-RapidAPI-Key': '04ca4f929cmshcdf617acc72fb57p17f6f1jsncbf2ac94bde8'
        }
    };

    
  return   fetch(`https://community-purgomalum.p.rapidapi.com/json?text=${textArea.value}&add=hate%20die%20%20kill%20strangle%20prostitute`, options)
        .then(response => response.json())
       .then( async function(response)
       {
          outResponse = `${response.result}`;
          const stringArray = `${outResponse.split(" ")}`;
          console.log(stringArray)
          for(let i=0; i < stringArray.length-1; i++)
           {
               if(stringArray[i].includes('*'))
               {
                   console.log(stringArray[i])
                   swal({
                    icon: 'error',
                    title: 'Cannot Accept!',
                    text: 'Your Post Contains Bad Contents That Are Unacceptable! ',
                 })
               }
               else
               {
                   SendRequestToMyServer();
                   break;
               }
            }
          console.log(outResponse)
       })
        .catch(err => console.error(err));
}
async function SendRequestToMyServer()
{
    let token = localStorage.getItem('Token');
    console.log(token);
    let formData = new FormData(form);
    let host= 'https://localhost:5001'
   fetch(`${host}/api/ApplicationUserPost/CreatePost`,{
       method:'POST',
       body:formData,
       headers:{
        "Authorization": 'Bearer ' + token,
      }
   }) 
    .then(function(response)
    {
        return response.json();
    })
   .then(function(final)
   {
       console.log(final)
       console.log(final.data.postId)
       swal({
        icon: 'success',
        title: 'Post Created Successfully!',
       });
       CreatePostLog(host,final.data.postId);
   })
   .catch(function(err)
   {
       console.error(err);
   });
}

function CreatePostLog(host,postId)
{
    // var DevelopmentPort='http://127.0.0.1:5501/'
    let url='http://127.0.0.1:5501/HTML/Discussion/GetDiscussionById.html/'+ postId;
    console.log(url);
    data={
        "postUrl": url,
         "postId": postId
    }
    console.log(data)
    fetch(`${host}/api/PostLog/CreatePostLog`,{
        method:"POST",
        body:data,
        headers:{
            "Content-Type":'application/json',
        }
    })
    .then(function(result)
    {
        return result.json();
    })
    .then(function(response)
    {
        console.log(response)
    })
    .catch(function(err)
    {
        console.error(err);
    })
}
