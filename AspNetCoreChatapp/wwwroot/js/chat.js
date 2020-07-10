"use strict"

//Initalize Dom Element
const formElem = document.querySelector('#form');
const textarea = document.querySelector('textarea');
const listOfMessages = document.querySelector('.messages');
let submitBtn = document.getElementById('submit');
let SendAudio = new Audio("./sounds/send-message.mp3")
let ReceiveAudio = new Audio("./sounds/receive-message.mp3")
let MessagesList = document.querySelector("div.messages")


/**
 * Make sure that the list of chats always scroll to last message
 */
function ScrollToLastChild() {
    let offset = MessagesList.lastElementChild.offsetTop
    MessagesList.scrollTop = offset
}

ScrollToLastChild()

//Define and connect to SignalR
let connection =  new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("ReceiveMessage" , function (message,num) {
    let div = document.createElement("div")
    div.innerHTML = `<p>${message.text}<br><em>${message.createAt}</em></br></p>`
    if(num == "1"){
        div.className = "message me"
    }else{
        div.className = "message y"

        //Paly Receive audio sound
        ReceiveAudio.play()
    }
    
    //Add the new message tp messages list
    listOfMessages.appendChild(div)

    ScrollToLastChild()
})

connection.start().then(function () {
    submitBtn.disabled  =  false
    console.log("Sucess Connect")
}).catch(e => {
    console.error(e.toString())
})


formElem.addEventListener('submit', async (e) => {
    // on form submission, prevent default
    e.preventDefault();

    //Empety textarea
    textarea.value = ""

    // construct a FormData object, which fires the formdata event
    var formData = new FormData(formElem)
    var json = {}
    for (const [key , value] of formData) {
        json[key] = value
    }

    connection.invoke("SendMessage", json.UserID , json.OtherUserID ,  json.ChatID ,  json.Text)
    
    //Paly Send audio sound
    SendAudio.play()
})





