var word = document.getElementById('word');
var definition = document.getElementById('defcont');
var fl = document.getElementById('fl');
var wordUsageUl = document.getElementById('word-usage');
var etymologyContent = document.getElementById('etymology-Content');
var audio = document.getElementById('audio');
var audiobtn = document.getElementById('playaudio');
var oralRep = document.getElementById('oralRep');
var oral = document.getElementById('oral');
var synonymsContainer = document.getElementById('synonyms');
var antonymsContainer = document.getElementById('antonyms');
var loginLi=  document.getElementById('login');
var text;
UnlockUserProfile();
fetch('https://random-word-api.herokuapp.com/word?number=1&swear=0')
    .then(function(finalResult) {
        return finalResult.json()
    })
    .then(function(data) {
        console.log(data);
        text = data[0].toLowerCase();
        console.log(text);
        word.innerHTML = 'Ace';
        SendRequest('ace');
        sendThesaurusRequest('ace');
    })
    .catch(function(ex) {
        console.error(ex, "Could Not Reach");
    })
const host = 'https://localhost:5001';

function SendRequest(text) {

    const requestUrl = `${host}/api/Oxford/ConnectToOxford?word=${text}`;
    console.log(requestUrl);
    fetch(requestUrl)
        .then(function(result) {
            return result.json();
        }).then(function(response) {
            console.log(response)
            definition.innerHTML = response.results[0].lexicalEntries[0].entries[0].senses[0].definitions[0];
            fl.innerHTML = (response.results[0].lexicalEntries[0].lexicalCategory.text).toUpperCase();
            console.log(definition);
            if (response.results[0].lexicalEntries[0].entries[0].senses[0].examples === undefined) wordUsageUl.style.visibility = "hidden";

            for (let i = 0; i < 2; i++) {
                var elementLi = elementCreator('li', 'usagelis');
                elementLi.style.color = "black";
                var resultLi = response.results[0].lexicalEntries[0].entries[0].senses[0].examples[i].text;
                elementLi.innerHTML = `<i> ${resultLi} </i>`;
                wordUsageUl.appendChild(elementLi);
                console.log(wordUsageUl);
            }
            etymologyContent.innerHTML = response.results[0].lexicalEntries[0].entries[0].etymologies[0];
            var audiofile = response.results[0].lexicalEntries[0].entries[0].pronunciations[0].audioFile;
            audio.setAttribute('src', audiofile);
            audiobtn.appendChild(audio);
            console.log(audio);
            var spanofdialect = elementCreator('span', 'spans');
            spanofdialect.innerHTML = `<b> (<i>${response.results[0].lexicalEntries[0].entries[0].pronunciations[0].dialects[0]} </i>) </b>`;
            oralRep.appendChild(spanofdialect);
            oral.innerHTML = `<b> Pronounced:
            <i style="color:black; font-weight:bolder;">[/"${(response.results[0].lexicalEntries[0].entries[0].pronunciations[0].phoneticSpelling).toUpperCase()}"/]</i> In <i>${response.results[0].lexicalEntries[0].entries[0].pronunciations[0].phoneticNotation} Standard </i> </b>`
            oralRep.appendChild(oral);

        })
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
                elementLi.innerHTML = `<a href="/HTML/ThesaurusPage.html" style="color:black"> ${finalResult[0].meta.syns[0][i]}</a>`;
                synonymsContainer.appendChild(elementLi);

            }
            for (let i = 0; i < finalResult[0].meta.ants[0].length; i++) {

                var elementLi = elementCreator('li', 'antonymLis');
                elementLi.innerHTML = `<a href="/HTML/ThesaurusPage.html" style="color:black"> ${finalResult[0].meta.ants[0][i]}</a>`;
                antonymsContainer.appendChild(elementLi);
            }
        })
}
function UnlockUserProfile()
{
    // if(localStorage.getItem('Token') === undefined)
    // {
    //     userProfile.style.visibility="hidden";
    // }
    if(localStorage.getItem('Token')=== null)
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
// SendRequest('ace');