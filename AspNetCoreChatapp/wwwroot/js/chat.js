"use strict"

const formElem = document.querySelector('#form');
const textarea = document.querySelector('textarea');
const listOfMessages = document.querySelector('.messages');
let submitBtn = document.getElementById('submit');

//Define and connect to SignalR
let connection =  new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
submitBtn.disabled = true

connection.on("ReceiveMessage" , function (message,num) {
    textarea.value = ""
    let div = document.createElement("div")
    div.innerHTML = `<p>${message.text}</p>`
    if(num == "1"){
        div.className = "message me"
    }else{
        div.className = "message y"
    }
    listOfMessages.appendChild(div)
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

    // construct a FormData object, which fires the formdata event
    var formData = new FormData(formElem)
    var json = {}
    for (const [key , value] of formData) {
        json[key] = value
    }
    console.log(json)

    // $.post("api/Chat/PostMessage",json)
    //     .done(function(data){
    //         textarea.value = ""
    //         console.log(data)
    //     });
    connection.invoke("SendMessage", json.UserID , json.OtherUserID ,  json.ChatID ,  json.Text)
})


