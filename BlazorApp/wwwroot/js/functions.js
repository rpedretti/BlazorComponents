function dragStart(event) {
    event.dataTransfer.setData("text/plain", event.srcElement.innerHTML);
    event.dataTransfer.effectAllowed = "copyMove";
    console.debug('drag start', event);
}

function dragEnter(event) {
    event.dataTransfer.dropEffect = "move";
    console.debug('drag enter', event);
}

function dragDrop(event) {
    const data = event.dataTransfer.getData("text/plain");
    event.target.innerHTML = data;
    console.debug('drag drop', data);
}