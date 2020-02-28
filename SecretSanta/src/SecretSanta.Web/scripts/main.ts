import '../styles/site.scss';

import { GiftList } from "./list-Gifts";


console.log("Before render gifts");

(new GiftList().renderGifts());

function searchByTitle() {
    alert("searchByTitle() called");
    (new GiftList().searchGifts());
}

console.log("After Srender gifts");

let btn = document.getElementById("searchButton");
btn.addEventListener("click", (e: Event) => searchByTitle());

function myFunction() {
    alert("onclick() called");
}
