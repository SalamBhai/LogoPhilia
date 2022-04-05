var headpage = document.getElementById("asd");
// var landingcss = document.createElement("link");
// landingcss.rel = "stylesheet";
// landingcss.href = "DictionaryLanding.css";
// landingcss.setAttribute('id', "dland");
// var resultcss = document.getElementById("dresult");
// // headpage.removeChild(resultcss);
// headpage.appendChild(landingcss);

var definationsContainer = document.getElementById("DefinationsContainer");
var searchBox = document.getElementById("searchBox");

var containerinf = document.getElementById("containerinf");
var searchbtn = document.getElementById("searchbtn");
var landingcss = document.getElementById("dland");
var word = document.getElementById("wordSearched");
var firstConcern = document.getElementById("firstConcern");
var secondConcern = document.getElementById("secondConcern");
var oralRep = document.getElementById("oralRep");
var audioBtncontainer = document.getElementById("buttonCont");
var pronunciation = document.getElementById("pron");
var SlsAndOral = document.getElementById("slsAndoral");
var searchForm = document.getElementById("searchform");
var definationHead = document.getElementById("defi");
var partOfSpeech = document.getElementById("fl");
var upinS2 = document.getElementById("up");
var shortDefination = document.getElementById("defcont");
var useInSentence = document.getElementById("useHead");
var theUse = document.getElementById("usage");
var initial = document.getElementById("init");
var initialContainer = document.getElementById("initial");
var partofSpeech2 = document.getElementById("fl2");
var wordInSecondConcern = document.getElementById("wordinsc");
var Syncontainer = document.getElementById("Syncontainer");
var antonymsCont = document.getElementById("antonymsCont");
var meaningAndUsage = document.getElementById("meaningandusage");
var meanings = document.getElementById("meanings");
var audioPron = document.getElementById("pronunciation");
var prevandNext = document.getElementById("prevandnext");
var next = document.getElementById("chevnex");
var audioBtn = document.getElementById("btn");
var srcofAudio = document.getElementById("src");
var userprofile = document.getElementById("userprofile");
var navbar = document.getElementById("navbar");

var sidebar = document.getElementById("sidebar");
var FirstKnownUseContent = document.getElementById("FirstKnownUseContent");
var EtymologyContent = document.getElementById("EtymologyContent");
var synonymsContents = document.getElementById("synonymsContents");
var antonymsContents = document.getElementById("antonymsContents");
var Synonymshead = document.getElementById("Synonyms");
var antonym = document.getElementById("ant");
var fronttems = document.getElementById("fi");
var sides = document.getElementById("sides");
var headofInflection = document.getElementById("headInf");
var inflections = document.getElementById("inflections");
var inflectionsAndFirstKnownUse = document.getElementById("inflectionsAndFirstKnownUse");
var synAndCite = document.getElementById("synAndcite");
var secondConcerns = document.getElementsByClassName('secondConcern');
var CharArrayOfEtymologyContents;
console.log(srcofAudio);
const responseArray = [];

searchbtn.addEventListener('click', function(e) {
            
            e.preventDefault();
            var text = searchBox.value;
            console.log(text);
   
            alert(`"Please wait while I search ${text}"`);
            fetch(`https://www.dictionaryapi.com/api/v3/references/collegiate/json/${text}?key=ea90f273-699f-4d3f-9ece-8ae6a91f0eda`)
                .then(function(searchResult) {
                    return searchResult.json();
                })
                .then(async function(finalResult) {

                        for (let response of finalResult) {
                            responseArray.push(response);
                        }
                        console.log(responseArray);
                        word.innerHTML = finalResult[0].hwi.hw;
                        oralRep.textContent = `${`["${finalResult[0].hwi.prs[0].mw}"]`}`;
                        shortDefination.innerHTML = `"<i>${finalResult[0].shortdef[0]}</i>"`;
                        partOfSpeech.innerHTML = `<i>${finalResult[0].fl}:</i>`;
                        if(finalResult[0].quotes !== undefined)  theUse.innerHTML = finalResult[0].quotes[0].t;
                        else 
                        {
                            theUse.style.visibility = "hidden";
                            useInSentence.style.visibility = "hidden";
                        }
                        
                     if((finalResult[0].hwi.prs[0].sound.audio).slice(0,2) ==="gg")  srcofAudio.setAttribute("src", `https://media.merriam-webster.com/audio/prons/en/us/mp3/gg/${finalResult[0].hwi.prs[0].sound.audio}.mp3`);
                     else if((finalResult[0].hwi.prs[0].sound.audio).slice(0,3) ==="bix")  srcofAudio.setAttribute("src", `https://media.merriam-webster.com/audio/prons/en/us/mp3/bix/${finalResult[0].hwi.prs[0].sound.audio}.mp3`);
                     else
                     {
                        srcofAudio.setAttribute("src", `https://media.merriam-webster.com/audio/prons/en/us/mp3/${finalResult[0].hwi.prs[0].sound.audio[0]}/${finalResult[0].hwi.prs[0].sound.audio}.mp3`);
                     }

                         console.log("src", srcofAudio);
                          audioBtn.appendChild(srcofAudio);
                          
                          if(finalResult[0].meta.offensive === true)  SlsAndOral.setAttribute("placeholder",`"${"Offensive Word"}"`);
                          else
                          {
                            SlsAndOral.setAttribute("placeholder",`"${"Not Offensive Word"}"`);
                          }
                         setItemsToSecondConcern(finalResult,text);
                          headofInflection.innerHTML = ` Change In Form Of ${text} in <i>${(finalResult[0].fl).toUpperCase()}</i>`
                          FirstKnownUseContent.innerHTML = ` The First Known Use Of ${text} was  <i>${finalResult[0].date}</i> in the above defination `;
                          EtymologyContent.innerHTML =  `<i>${finalResult[0].et[0][1]}:</i>`;
                          CharArrayOfEtymologyContents = Array.from(finalResult[0].et[0][1]);
                         
                         if(finalResult[0].et[0][1] === null )  EtymologyContent.style.visibility =  "hidden";
                           
                       
                         if(finalResult[0].syns !== undefined)
                         {
                            for (let i = 0; i < 1; i++) 
                            {
                               
                               var syn = document.createElement('li');
                               syn.classList.add("synos");
                               syn.innerHTML = finalResult[0].syns[0].pt[i][1];
                               synonymsContents.appendChild(syn);
                               Syncontainer.appendChild(synonymsContents)
                               secondConcern.appendChild(Syncontainer);
                               definationsContainer.appendChild(secondConcern);
                               i+=1;
                               if(i===4 && finalResult[0].syns[0].pt.length > 3) break;
                            }
                             creatorOfPtag('Synonyms',text);
                         }
                        else
                        {  
                          creatorOfPtag('Synonyms',text);
                        }
                        antonym.innerHTML = ` Please Visit the  <a href="Thesauruspage.html"> Thesaurus page</a> for antonyms of the word ${text}</p>`;
                        antonymsContents.appendChild(antonym);
                        antonymsCont.appendChild(antonymsContents)
                        secondConcern.appendChild(antonymsCont);
                            if(finalResult[0].ins !== undefined && finalResult[0].fl ==="verb")
                            {
                              for (let i = 0; i <finalResult[0].ins.length; i++) 
                              {
                               if((finalResult[0].ins[i].if).slice(finalResult[0].ins[i].if.length-2,finalResult[0].ins[i].if.length) === "ed")
                                {
                                  var li = document.createElement('li');
                                  li.innerHTML = `<span style=" font-weight:bold; font-size:16px;color:darkblue"> Past Tense : </span>  <span style="font-style:italic; font-weight:bolder;font-size:18px"> ${(finalResult[0].ins[i].if).toUpperCase()} </span> `
                                  containerinf.appendChild(li);
                                }
                                if((finalResult[0].ins[i].if).slice(finalResult[0].ins[i].if.length-3,finalResult[0].ins[i].if.length) === "ing")
                                {
                                   var li = document.createElement('li');
                                   li.innerHTML = `<span style=" font-weight:bold; font-size:16px;color:darkblue"> Present Continuous Tense : </span>  <span style="font-style:italic; font-weight:bolder;font-size:18px"> ${(finalResult[0].ins[i].if).toUpperCase()} </span> `
                                   containerinf.appendChild(li);
                                }
                              }
                            }
                            else
                            {
                              inflections.style.visibility = "hidden";
                            }
                            if(responseArray.length>1)
                            {
                              definationsContainerForEachPartOfSpeech(text);
                            }
                            else
                            {
                              openPage();
                            }
                            
                            
                }) 
                .catch(function() {
                   console.error("Ona di");
                })      
})

var setItemsToSecondConcern = function(finalResult,text)
{
  for (let i = 0; i < finalResult[0].def[0].sseq.length; i++) 
  {
     var sensualMeaning = document.createElement('li');
     sensualMeaning.classList.add("meaning");
     sensualMeaning.innerHTML = finalResult[0].def[0].sseq[i][0][1].dt[0][1];
     meaningAndUsage.appendChild(sensualMeaning);
  }
  wordInSecondConcern.textContent = text;
  initial.textContent = (finalResult[0].fl[0]).toUpperCase();
  partofSpeech2.innerHTML = (finalResult[0].fl).toUpperCase();
 
 
}

var definationsContainerForEachPartOfSpeech =  function(text)
{
  
  
    for (let i = 1; i < responseArray.length; i++) 
    {
    
      var divofdefinition = elementCreator('secondConcern','div');
      var firstItemUl = elementCreator('firstItem','ul');
      var firstliInfirstItemUl = elementCreator('initial','li');
      var h3 = elementCreator('init','h3')
      h3.setAttribute('id', "init");
      h3.innerHTML = (responseArray[i].fl[0]).toUpperCase();
      firstliInfirstItemUl.appendChild(h3);
      var secondLiInFirstItemUl = elementCreator('fl2','li')
      secondLiInFirstItemUl.innerHTML = (responseArray[i].fl).toUpperCase();
      var liOfWord = elementCreator('wordinsc', 'li');
      liOfWord.innerHTML = text;
      var liOfChevronUp = elementCreator('up','li');
      liOfChevronUp.setAttribute('title','Click To View Content');
      liOfChevronUp.innerHTML =  `<i class="fas fa-chevron-up"></i>`;
      firstItemUl.appendChild(firstliInfirstItemUl);
      firstItemUl.appendChild(secondLiInFirstItemUl);
      firstItemUl.appendChild(liOfWord);
      firstItemUl.appendChild(liOfChevronUp);
      divofdefinition.appendChild(firstItemUl);
      var divofMeaningSynonymsAndAntonyms = elementCreator('meanings','div');
      var ulOfmeaninganduse =   iteratorOfResponseForMeaningForEachDiv(responseArray, i);
      divofMeaningSynonymsAndAntonyms.appendChild(ulOfmeaninganduse); 
     
      var divofSynonyms = document.createElement('div');
      var h4ofsynhead  =  elementCreator('Synonyms','h4');
      h4ofsynhead.innerHTML  = "Synonyms";
      var elementOfUlforsynonyms = elementCreator('synonymsContents','ul');
      var ptag = creatorOfPtag("Synonyms",text);
      elementOfUlforsynonyms.appendChild(ptag);
      divofSynonyms.appendChild(h4ofsynhead);
      divofSynonyms.appendChild(elementOfUlforsynonyms);
      divofMeaningSynonymsAndAntonyms.appendChild(divofSynonyms);
      var antonymDiv = document.createElement('div');
      var h4ofantonymhead  =  elementCreator('Antonyms','h4');
      h4ofantonymhead.innerHTML  = "Antonym"
      var elementOfUlforanto = elementCreator('antonymsContents','ul');
      var ptag = creatorOfPtag("Antonyms",text);
      elementOfUlforanto.appendChild(ptag);
      antonymDiv.appendChild(h4ofantonymhead);
      antonymDiv.appendChild(elementOfUlforanto);
      divofMeaningSynonymsAndAntonyms.appendChild(antonymDiv);
      divofdefinition.appendChild(divofMeaningSynonymsAndAntonyms);
      definationsContainer.appendChild(divofdefinition);
      console.log(definationsContainer);
      openPage();
    }
  
}
var openPage = function()
{
  headpage.removeChild(landingcss);
 
 fronttems.style.visibility ="hidden"
 firstConcern.style.visibility= "visible";
 definationsContainer.style.visibility = "visible";
 userprofile.style.visibility = "visible";
 navbar.style.visibility = "visible";
 sides.style.visibility = "visible";

}
var iteratorOfResponseForMeaningForEachDiv =  function(responseArray, index)
{
  
  var meaningAndUsageUl = elementCreator('meaninganduse', 'ul');
  for (let j = 0; j < responseArray[index].def[0].sseq.length; j++) 
  {
    var liOfMeaning  = elementCreator('meanings','li');
    liOfMeaning.innerHTML  = responseArray[index].def[0].sseq[j][0][1].dt[0][1];
    meaningAndUsageUl.appendChild(liOfMeaning);
    // console.log("test",meaningAndUsageUl);
  }
  for (let secondConcern of secondConcerns) {
   if( responseArray[index].def[0].sseq.length < 4 && responseArray[index].syns === undefined)
   {
     secondConcern.style.height = "62vh";
   }
    
  }
  return meaningAndUsageUl;
  
}

var creatorOfPtag  = function(synoyms,text)
{
  var ptag = document.createElement('p');
  ptag.style.fontSize= "19px";
  ptag.style.marginTop = "15px";
  ptag.style.fontStyle= "italic";
  ptag.style.color= "dodgerblue";
  ptag.innerHTML  = ` Please Visit the  <a href="Thesauruspage.html"> Thesaurus page</a> for ${synoyms} of the word ${text}...</p>`;
  synonymsContents.appendChild(ptag);
  Synonymshead.style.visibility = "visible";
  synonymsContents.style.visibility ="visible";
  return ptag
}
var elementCreator = function(className,elementName)
{
  var element = document.createElement(`${elementName}`);
  element.classList.add(className);
  return element;
}
navbar.addEventListener("click", function()
{
  console.log(navbar);
  sidebar.style.visibility = "visible";
  navbar.style.visibility = "hidden";
  sidebar.addEventListener("click",function()
  {
      sidebar.style.visibility = "hidden";
      navbar.style.visibility = "visible";
  })
  setTimeout(function()
  {
      sidebar.style.visibility = "hidden";
      navbar.style.visibility = "visible";
  },50000)              
})