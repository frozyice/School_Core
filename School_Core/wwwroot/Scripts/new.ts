
function reverse() : void {
    let content: string = (<HTMLInputElement>document.getElementById("inputSentence")).value.toLowerCase()
    let ContentCollection: string[] = content.split(" ")
    ContentCollection.reverse()
    
    let reversedContent = ContentCollection.toString()
    let newContent = reversedContent.replace(/[,]/g, " ")
    
    document.getElementById("outputSentence").innerHTML = newContent
    
}

document.getElementById("inputSentence").addEventListener("keyup", reverse)

window.onload = function (){
    reverse()
}