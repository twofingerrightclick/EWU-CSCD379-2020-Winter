import '../styles/site.scss';

import { GiftList } from "./list-Gifts";


    (new GiftList().renderGifts());
 


function searchByTitle() {
    (new GiftList().searchGifts());
}

function reloadList() {
    (new GiftList().reloadList());
}


let btn = document.getElementById("searchButton");
btn.addEventListener("click", (e: Event) => searchByTitle());

let cancelButton = document.getElementById("cancelButton");
btn.addEventListener("click", (e: Event) => reloadList());

