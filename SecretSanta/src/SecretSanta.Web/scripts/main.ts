import '../styles/site.scss';

import { GiftList } from "./list-Gifts";




var giftsRendered: boolean = false;

if (!giftsRendered) {
    (new GiftList().renderGifts());
    giftsRendered = true;
}


function searchByTitle() {
    (new GiftList().searchGifts());
}



let btn = document.getElementById("searchButton");
btn.addEventListener("click", (e: Event) => searchByTitle());

