const [titleInput, textArea, submitButton, form] = [
    document.querySelector('#title-Input'), document.querySelector('#postContent'), document.querySelector('#submitButton'),document.querySelector('#form')]

    const host = 'https://localhost:5001'
    const id = parseInt(localStorage.getItem('postId'));
    console.log(id);
  
   
    
    
function GetData() 
{
    fetch(`${host}/api/ApplicationUserPost/GetPost/${id}`)
    .then(function(response)
    {
        return response.json();
    })
    .then(function(result)
    {
        console.log(result.data);
        textArea.value = result.data.postContent;
        if(result.data.title === null)
        {
           titleInput.setAttribute('placeholder', 'No Title');
        }
        else
        {
            titleInput.value = result.data.title;
        }
    })
    .catch(function(err)
    {
        console.error(err);
    })
}
    form.addEventListener('submit', async function(e)
    {
        e.preventDefault();
        const data = {
            postContent: textArea.value,
            postTitle: titleInput.value,
        }
     
         submitButton.setAttribute('disabled', true);
         submitButton.style.cursor="not-allowed";
          //    await FilterPostContent();
          SendRequestToMyServer(data);
     //     console.log(outResponse);
       console.log(data);
    })

    
    function SendRequestToMyServer(data)
    {
      fetch(`${host}/api/ApplicationUserPost/UpdatePost/${id}`,{
        method: "PUT",
        body:JSON.stringify(data),
        headers:{
            "Content-type": 'application/json'
        }
      })
      .then(function(response)
      {
          return response.json();
      })
      .then(function(result)
      {
          console.log(result);
          if(result.success)
          {
              alert(`Update Successfully made to post with Id ${id}`);
              
          }
      })
      .catch(function(err)
      {
          console.error(err);
      })    
    }
GetData();