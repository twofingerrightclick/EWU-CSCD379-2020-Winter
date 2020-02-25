import {
    IGiftClient,
    GiftClient,
    Gift
} from "./secret-santa-api.client";

export const hello = () => "Hello world!";

export class App {
    async renderGifts() {
        var gifts = await this.getAllGifts();
        const itemList = document.getElementById("itemList");
        for (let index = 0; index < gifts.length; index++) {
            const gift = gifts[index];
            document.write("Hello World");
            const giftList = document.createElement("li");
            giftList.textContent = `${gift.title}:${gift.description}`;
            itemList.append(giftList);
        }
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
