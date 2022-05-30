const[discussionContainer, inputForPage] = [document.querySelector('#discussions-container'), document.getElementById('posts-input')];

let arrayOfData;
let arrayOfPageContents = [];

const host = 'https://localhost:5001';

var elementInitial = ElementWithSingleClass('div', 'initialElement');
  elementInitial.style.opacity="0.3"; elementInitial.innerText = "Please Use The Input Box above To Select the number of contents to be displayed here";
   discussionContainer.appendChild(elementInitial);

function GetAllDiscussions()
{
  fetch('https://localhost:5001/api/ApplicationUserPost/GetAllPosts')
  .then(function(result)
  {
      return result.json();
  })
  .then(function(response)
  {
    console.log(response);
    console.log(response.data)
    arrayOfData = Array.from(response.data);
    // DoDisplay(response.data);
    console.log(arrayOfData);
  })
  .catch(function(err)
  {
     console.error(err);
  })
} 

inputForPage.addEventListener('change', SetArrayOfPageContents)

function SetArrayOfPageContents()
{
  var inputElementValue = inputForPage.value;
  
  discussionContainer.innerHTML = "";
  arrayOfPageContents = [];
  for(let i = 0; i < inputElementValue; i++ )
  {
    arrayOfPageContents.push(arrayOfData[i]);
  }
  inputForPage.setAttribute('max', arrayOfData.length);
  inputForPage.setAttribute('min', 1);
  DoDisplay(arrayOfPageContents);
  console.log(arrayOfPageContents);

}

function DoDisplay(arrayOfPageContents)
{
   
    arrayOfPageContents.forEach(data => {
      const [discussion, firstChild, user, photoHolder, CreatorPhoto, UserName, postTopicDiv,

        postTitleSpan, SecondChildDiv, postContent, ViewButtonDiv, button, link, thirdChild, CommentDiv,
   
        countSpan, IconSpan, SendCommentDiv, sendIconSpan, dateDiv, CreateComment, dateText, dateValue , postActions
 ] = 
   
   [ ElementWithSingleClass('div','discussion'), ElementWithSingleClass('div','firstChild')
    ,ElementWithSingleClass('div','user'), ElementWithSingleClass('div','photo-parent'),ElementWithSingleClass('img','photo'),
    ElementWithSingleClass('div','username'), ElementWithSingleClass('div','post-topic'),ElementWithSingleClass('span','title')
    ,ElementWithSingleClass('div','second-child'), ElementWithSingleClass('div','post-content'),
    ElementWithSingleClass('div','view-button'),ElementWithSingleClass('button','button'), 
    document.createElement('a'), ElementWithSingleClass('div','third-child'), 
    ElementWithSingleClass('div','content'), ElementWithSingleClass('span','count')
    ,document.createElement('span'), CreateMultipleClassElement('div','content','send-comment'),
    ElementWithSingleClass('span', 'send-icon'), CreateMultipleClassElement('div', 'content', 'date'),
     InputElement('Create Comment', 'button'), document.createElement('span'), document.createElement('span'), document.createElement('button')
   ]
     link.href="/HTML/Discussion/GetDiscussionById.html";

     IconSpan.textContent = "Comments";
     IconSpan.innerHTML = `<i class="fa-solid fa-comments count-icon"></i> ${"Comments"}`;
     

  CreatorPhoto.setAttribute('src', `https://localhost:5001/UserImages/${data.creatorPhoto}`); CreatorPhoto.setAttribute('alt', `Creator Photo`);
  photoHolder.appendChild(CreatorPhoto); 
  user.appendChild(photoHolder);
  UserName.textContent = data.applicationUserName; 
  user.appendChild(UserName);
  firstChild.appendChild(user);
  postTitleSpan.textContent= "Post Title:";
  postTopicDiv.appendChild(postTitleSpan);
 if(data.title !== null) postTopicDiv.textContent = data.title;
 else if(data.title === null ) {postTopicDiv.textContent = "No Topic"; postTopicDiv.style.opacity = 0.5;}
 
  firstChild.appendChild(postTopicDiv);
  discussion.appendChild(firstChild);
  postContent.innerHTML = data.postContent;
  SecondChildDiv.appendChild(postContent);
  button.appendChild(link); 
  button.addEventListener('click', function()
  {
    localStorage.removeItem('postId');
   localStorage.setItem('postId', data.postId);
   location.href = "/HTML/Discussion/GetDiscussionById.html";
  })
  button.textContent = "View More About Post" 
  ViewButtonDiv.appendChild(button);
  SecondChildDiv.appendChild(ViewButtonDiv);
  discussion.appendChild(SecondChildDiv);
  countSpan.textContent = data.applicationUserComments.length;
  CommentDiv.appendChild(countSpan);
  CommentDiv.appendChild(IconSpan);
  thirdChild.appendChild(CommentDiv);
  SendCommentDiv.appendChild(CreateComment);
  SendCommentDiv.appendChild(sendIconSpan);
  thirdChild.appendChild(SendCommentDiv);
  dateText.textContent = "Date Created: "
  dateDiv.appendChild(dateText);
  dateValue.textContent = data.datePosted.split("T")[0];
  dateDiv.appendChild(dateValue);
  postActions.classList.add('actionbutton');
  postActions.setAttribute('title','More Actions..')
  postActions.innerHTML = `<i class="fas fa-bars" aria-hidden="true"></i>`
  postActions.childNodes.forEach(element => element.style.color = "darkblue");
  postActions.addEventListener('click', function()
    {
      console.log(1);
       var ulOfActions = ElementWithSingleClass('ul', 'ul-of-actions');
       var li1 =  ElementWithSingleClass('li','actionLi');
       li1.innerHTML = 
       `${spanContent('<a href="/HTML/Discussion/GetDiscussionById.html" style="color:white"> View Post</a>')} 
       ${spanContent('<i class="fas fa-bars" aria-hidden="true"></i>')}`;
       
       ulOfActions.appendChild(li1);

       console.log(ulOfActions);
       thirdChild.appendChild(ulOfActions);

       if(data.videoFile !== null)
       {
         var li2 = ElementWithSingleClass('li','actionLi');
         li2.innerHTML = 
         `${spanContent('<a href="/HTML/Discussion/GetDiscussionById.html" style="color:white"> View Video</a>')}`;
         ulOfActions.appendChild(li2);
       }
       if( data.applicationUserName === localStorage.getItem('UserName'))
       {
          var li3 = ElementWithSingleClass('li','actionLi');
          li3.innerHTML = 
          `${spanContent('<a href="/HTML/Discussion/EditDiscussion.html" style="color:white"> Edit Post</a>')}`;
          var li4 = ElementWithSingleClass('li','actionLi');
          li4.innerHTML = 
          `${spanContent('<a style="color:white; cursor: pointer"> Delete Post</a>')}`;
          ulOfActions.appendChild(li3);
          ulOfActions.appendChild(li4);
          EditPost(li3, data.postId);
          DeletePost(li4, data.postId);
       }
    })
  thirdChild.appendChild(dateDiv);
  thirdChild.appendChild(postActions);
  discussion.appendChild(thirdChild);
  discussionContainer.appendChild(discussion);
})
console.log(discussionContainer);
}

const UserToken = GetLoginToken();
function EditPost(li3, postId)
{
  li3.addEventListener('click',function()
  {
     localStorage.removeItem('postId');
     localStorage.setItem('postId', postId);
  })
}
function DeletePost(li4,postId)
{

   li4.addEventListener('click', function()
   {
     console.log(postId,'Click...')
    //  swal({
    //     icon: 'error',
    //    text: 'Sure To Delete Post?',
    //  })
     alert('Sure To Delete Post?')
      fetch(`${host}/api/ApplicationUserPost/DeletePostOfLoggedInUser/${postId}`, {
        method: "DELETE",
        headers:{
          "Authorization": 'Bearer ' + UserToken,
          "Content-Type": 'application/json'
        }
      })
      .then(function(resp)
      {
         return resp.json();
      })
      .then(function(result)
      {
        console.log(result);
        location.reload();
      })
      .catch(function(err)
      {
        console.error(err);
      })
   })
}
function spanContent(content)
{
  var span = `${content}`;
  return span;
}

function CreateMultipleClassElement(elementName, className,className2)
{
    var element = document.createElement(`${elementName}`)
    element.classList.add(className); element.classList.add(className2)
    return element;
}
function ElementWithSingleClass(elementName,className)
{
    var element = document.createElement(`${elementName}`);   element.classList.add(className); return element;
    
}
function InputElement(value, type)
{
    var InputElement = document.createElement('input'); InputElement.value = `${value}`; InputElement.type = `${type}`
    return InputElement;
}
GetAllDiscussions();
