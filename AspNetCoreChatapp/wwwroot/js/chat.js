const formElem = document.querySelector('#form');
const textarea = document.querySelector('textarea');
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

    $.post("api/Chat/PostMessage",json)
        .done(function(data){
            textarea.value = ""
            console.log(data)
        });
})