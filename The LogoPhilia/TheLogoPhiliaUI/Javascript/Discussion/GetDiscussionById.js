

const [activeLink, cardImage, postCreatorName, creatorMail,
postTitle, datePosted, postVideo, commentingSection, textArea, reactionButton, reactionsChoice,
 postContent, postContainer, commentsParent] = [document.querySelector('#post-title'),
document.querySelector('.photo'), document.querySelector('#creator'), document.querySelector('.email'),
document.querySelector('.post-title'), document.querySelector('.date-value'),document.querySelector('#postVideo')
, document.querySelector('.comment-section'), document.querySelector('textarea'), document.querySelector('.react'), 
document.querySelector('.reactions'),document.querySelector('.content'), document.querySelector('.post'),
document.querySelector('.comments')]


reactionButton.addEventListener('click',function()
{
  reactionButton.style.backgroundColor = "darkblue";
  reactionButton.style.color = "cyan";
   setTimeout(function()
   {
    reactionsChoice.style.visibility ="visible";
  
   },1000)
})

const host = "https://localhost:5001";
var postId = parseInt(localStorage.getItem('postId'));
let url =`${host}/api/ApplicationUserPost/GetPost/${postId}`;
console.log(url);
fetch(`${url}`)
.then(function(result)
{
    return result.json();
})
.then(function(response)
{
    console.log(response);
    SetPostContent(response.data)
    SetCommentSection(response.data.applicationUserComments)
})
.catch(function(err)
{
    console.error(err);
})

var date = new Date();
const daysInYear = [31,28,31,30,31,30,31,30,31,31,30,31];

function SetPostContent(data)
{
    var shortDate = `${data.datePosted.split('T')[0]}`;
    var dayPosted = parseInt(shortDate.split('-')[2]);
    var output = GetPostCreationSpan(parseInt(shortDate.split('-')[1]),dayPosted);
    console.log(output)
    datePosted.innerHTML = `${data.datePosted.split('T')[0]} <i>(${output}days ago) </i>`;
    postCreatorName.textContent = data.applicationUserName;
    creatorMail.textContent = data.applicationUserEmail;
   data.title !== null ? postTitle.textContent = data.title : postTitle.textContent = `${data.applicationUserName.toUpperCase()}' Post:`;
    data.title !== null? activeLink.textContent = data.title : activeLink.textContent = `${data.applicationUserName.toUpperCase()} Posted:`
    // data.videoFile !== null ? postVideo.setAttribute('src',`${data.videoFile}`) : postContainer.removeChild(postVideo);
    postContent.textContent = data.postContent;
    //  data.creatorPhoto!==null? cardImage.setAttribute('src',`${data.creatorPhoto}`): cardImage.setAttribute('alt',`Creator Image`);
   
}
function GetPostCreationSpan(month,datePosted)
{
  var alldaysWithinPostCreationTillCurrentMonth = 0;
  var daysInPostMonth = 0;
  var daysBeforePost = 0;
  var accurateAgoDate = 0;
 for (let i = month-1; i <= date.getUTCMonth(); i++) 
 {
  alldaysWithinPostCreationTillCurrentMonth+= daysInYear[i];
     daysInPostMonth = daysInYear[month-1];
 }

 var thisMonth = date.getMonth();
 console.log(date.getUTCDate());
 console.log(daysInYear[thisMonth])
 var daysAfterToday = daysInYear[thisMonth] - date.getUTCDate();
 console.log(daysAfterToday);
 daysBeforePost =  datePosted - 1;
 var daysAfterPostCreation =  alldaysWithinPostCreationTillCurrentMonth - daysBeforePost;
 console.log(alldaysWithinPostCreationTillCurrentMonth);
 var accurateAgoDate = daysAfterPostCreation - daysAfterToday;
 return accurateAgoDate-1;
}
function SetCommentSection(commentsList)
{
 for(let comment of commentsList)
   {
    var shortCommentDate = `${comment.commentDate.split('T')[0]}`;
    var dayCommented = parseInt(shortCommentDate.split('-')[2]);
     var result = GetPostCreationSpan(parseInt(shortCommentDate.split('-')[1]), dayCommented);
     commentsParent.innerHTML+= `<div class="comment">
        <div class="commenter">
          <img src="/assets/myImage.jpg" alt="Creator" srcset="">
        </div>
        <div class="comment-made">
          <div class="first-div">
              <div class="info">
                  <strong>${comment.applicationUserName}</strong> Said:
              </div>
              <div class="date-commented">
                ${comment.commentDate.split('T')[0] } <i> (${result}
                 days ago) </i>
              </div>
          </div>
          <div class="second-div">
             ${comment.commentContent}
          </div>
        </div>
     </div>`
   }
}