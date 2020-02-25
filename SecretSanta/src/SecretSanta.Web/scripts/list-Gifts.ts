import {
    IGiftClient,
    GiftClient,
    Gift
} from "./secret-santa-api.client";

export const hello = () => "Hello world!";

export class App {
    async renderGifts() {
        var gifts = await this.getAllGifts();
        const itemList = document.getElementById("giftList");
        gifts.forEach(gift => {
            const listItem = document.createElement("li");
            listItem.textContent = `${gift.id}:${gift.title}:${gift.description}:${gift.url}`
            itemList.append(listItem);
        })
    }

    giftClient: IGiftClient;
    constructor(giftClient: IGiftClient = new GiftClient()) {
        this.giftClient = giftClient;
    }

    async getAllGifts() {
        var gifts = await this.giftClient.getAll();
        return gifts;
    }
}
