import '../styles/site.scss';

import { GiftList } from "./list-Gifts";


console.log("Before render gifts");

(new GiftList().renderGifts());

console.log("After Srender gifts");

