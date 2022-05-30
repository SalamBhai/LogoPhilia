console.log("Hey God oo");
console.log(location.href);
console.log(document.location)
var hst= document.location.origin +`${document.location.pathname}?+${id=1}`;
console.log(hst)
const [word,definition, fl,wordUsageUl, etymologyContent,audio,audiobtn,oralRep, 
oral,synonymsContainer,antonymsContainer,loginLi, userProfile,antHead] =
 [document.getElementById('word'),document.getElementById('defcont'),
document.getElementById('fl'), document.getElementById('word-usage'),document.getElementById('etymology-Content'),
document.getElementById('audio'),document.getElementById('playaudio'),document.getElementById('oralRep'),
document.getElementById('oral'),document.getElementById('synonyms')
,document.getElementById('antonyms'),document.getElementById('login'), document.getElementById('userProfile'),document.getElementById('head')];


let text;
let Definition;
let etymologycontent;
let antonymLi;
let synonymLi;
let partOfSpeech;
let SpanOfDialectContent;
let resultLi;
let audiofile;
let Oral;
const host = 'https://localhost:5001';



function ActivateWordOfTheDayRequest()
{
    console.log("Activate Word Of The Day Running");
   var wordGotten = generateRandomWord();
   console.log(wordGotten);
        text = wordGotten;
        console.log(text);
        word.innerHTML = text;
        SendRequest(text);
        sendThesaurusRequest(text);
    
}


function SendRequestDaily()
{
    var time = 200000; //Milliseconds;
    ActivateWordOfTheDayRequest();
    setInterval(function()
    {
        location.reload();
        ActivateWordOfTheDayRequest();
        console.log("Executed...");
    },time);
    
    
}
function Wait()
{
    setTimeout(function()
    {
        location.reload();
        
    },5000)
}

function SendRequest(text) {

    const requestUrl = `${host}/api/Oxford/ConnectToOxford?word=${text}`;
    console.log(requestUrl);
    fetch(requestUrl)
        .then(function(result) {
            return result.json();
        }).then(GiveContentsToOxford)
        .catch(function(errr) {
            console.error("Could Not Fetch", errr);
        })
}

function elementCreator(elementName, className) {
    var element = document.createElement(`${elementName}`)
    element.classList.add(`${className}`);
    return element;
}

function sendThesaurusRequest(text) {
    fetch(`https://www.dictionaryapi.com/api/v3/references/thesaurus/json/${text}?key=c78174eb-e0d2-474d-b1d6-fb11d440c5b6`)
        .then(function(response) {
            return response.json();
        })
        .then(function(finalResult) {
            console.log(finalResult);
            for (let i = 0; i <= finalResult[0].meta.syns[0].length; i++) {
                if (i === 13) {
                    break;
                }
                var elementLi = elementCreator('li', 'synonymLis');
                synonymLi=`<a href="/HTML/ThesaurusPage.html" style="color:black"> ${finalResult[0].meta.syns[0][i]}</a>`;
                elementLi.innerHTML = synonymLi;
                synonymsContainer.appendChild(elementLi);

            }
           if(finalResult[0].meta.ants !== null)
           {
             for (let i = 0; i < finalResult[0].meta.ants[0].length; i++) 
             {

               var elementLi = elementCreator('li', 'antonymLis');
               antonymLi = `<a href="/HTML/ThesaurusPage.html" style="color:black"> ${finalResult[0].meta.ants[0][i]}</a>`;
               elementLi.innerHTML = antonymLi;
               antonymsContainer.appendChild(elementLi);
             }
           }
           else if(finalResult[0].meta.ants === null)
           {
               antHead.style.visibility="hidden";
             antonymsContainer.style.visibility= "hidden";
           }
        })
        .catch(function(err)
        {
          console.error(err);
        })
}
function UnlockUserProfile()
{
    
    if(localStorage.getItem('Token') === null)
    {
        console.log('I Exist')
        userProfile.style.visibility="hidden";
        loginLi.style.visibility="visible";
    }
    else if(localStorage.getItem('Token')!== null)
    {
        userProfile.style.visibility="visible";
        loginLi.style.visibility="hidden";
    }
}

function GiveContentsToOxford(response)
{
 console.log(response)
 Definition = response.results[0].lexicalEntries[0].entries[0].senses[0].definitions[0];
 definition.innerHTML="";
 definition.innerHTML = Definition;
 partOfSpeech = (response.results[0].lexicalEntries[0].lexicalCategory.text).toUpperCase();
 fl.innerHTML="";
 fl.textContent = partOfSpeech;
 console.log(definition);
 if (response.results[0].lexicalEntries[0].entries[0].senses[0].examples === undefined) wordUsageUl.style.visibility = "hidden";

 for (let i = 0; i < 2; i++) 
  {
    var elementLi = elementCreator('li', 'usagelis');
    elementLi.style.color = "black";
    resultLi = response.results[0].lexicalEntries[0].entries[0].senses[0].examples[i].text;
    resultLi.innerHTML="";
    elementLi.innerHTML = `<i> ${resultLi} </i>`;
    wordUsageUl.innerHTML="";
    wordUsageUl.appendChild(elementLi);
    console.log(wordUsageUl);
  }
    etymologycontent= response.results[0].lexicalEntries[0].entries[0].etymologies[0];
    etymologyContent.innerHTML="";
    etymologyContent.innerHTML = etymologycontent;
    audiofile="";
    audiofile= response.results[0].lexicalEntries[0].entries[0].pronunciations[0].audioFile;
    audio.setAttribute('src', audiofile);
    audiobtn.appendChild(audio);
    console.log(audio);
    var spanofdialect = elementCreator('span', 'spans');
    SpanOfDialectContent = `<b> (<i>${response.results[0].lexicalEntries[0].entries[0].pronunciations[0].dialects[0]} </i>) </b>`;
    spanofdialect.innerHTML = SpanOfDialectContent;
    oralRep.appendChild(spanofdialect);
    Oral=`<b> Pronounced:
        <i style="color:black; font-weight:bolder;">[/"${(response.results[0].lexicalEntries[0].entries[0].pronunciations[0].phoneticSpelling).toUpperCase()}"/]</i> In <i>${response.results[0].lexicalEntries[0].entries[0].pronunciations[0].phoneticNotation} Standard </i></b>`
    oralRep.appendChild(oral);
    oral.innerHTML = Oral;
    console.log(Definition,Oral,resultLi,antonymLi,etymologycontent,SpanOfDialectContent,audiofile,partOfSpeech)
}
UnlockUserProfile();
// SendRequestDaily();