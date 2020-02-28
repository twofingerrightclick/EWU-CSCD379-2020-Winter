import '../styles/site.scss';

import { GiftList } from "./list-Gifts";


console.log("Before render gifts");

(new GiftList().renderGifts());

function searchByTitle() {
    (new GiftList().searchGifts());
}

console.log("After Srender gifts");


function myFunction() {
    alert("onclick() called");
}
