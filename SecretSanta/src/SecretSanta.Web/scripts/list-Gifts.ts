﻿import {
    IGiftClient,
    GiftClient,
    IUserClient,
    UserClient,
    Gift,
    GiftInput,
    UserInput
} from "./secret-santa-api.client";

export const hello = () => "Hello world!";

export class GiftList {
    async deleteAllGifts() {
        var gifts = await this.getAllGifts();
        console.log("In render gifts");
        const itemList = document.getElementById("giftList");
        gifts.forEach(gift => {
            this.giftClient.delete(gift.id);

        })
    }

    async populateGifts() {
      

        await this.userClient.post(this.userThePrincessBride);

        await this.giftClient.post(this.buttercupGift);
        await this.giftClient.post(this.imGift);
        await this.giftClient.post(this.miracleMaxGift);
        await this.giftClient.post(this.dreadPirateRobertsGift);
        await this.giftClient.post(this.buttercupGift);

    }

    async renderGifts() {
        await this.deleteAllGifts();
        await this.populateGifts();

        var gifts = await this.getAllGifts();
        console.log("In render gifts");

        var tableBody =document.getElementById("tableBody");

      
        gifts.forEach(gift => {
            var tableRow = document.createElement("tr");
            
            let id = document.createElement("td");
            id.textContent = `${gift.id}`
            tableRow.append(id);

            let title = document.createElement("td");
            title.textContent = `${gift.title}`
            tableRow.appendChild(title);

            let desc = document.createElement("td");
            desc.textContent = `${gift.description}`
            tableRow.append(desc);

            let url = document.createElement("td");
            url.textContent = `${gift.url}`
            tableRow.append(url);

            tableBody.append(tableRow);
        })
    

    }




    giftClient: IGiftClient;
    userClient: IUserClient;
    constructor(giftClient: IGiftClient = new GiftClient(), userClient: IUserClient = new UserClient()) {
        this.giftClient = giftClient;
        this.userClient = userClient;
    }


    async getAllGifts() {
        var gifts = await this.giftClient.getAll();
        return gifts;
    }


    userThePrincessBride: UserInput = new UserInput({
        firstName: 'The Princess',
        lastName: 'Bride'
    });

  


    imGift:GiftInput = new GiftInput({
        title: 'spanish rapier',
        description: 'A Rapier is type of sword with a slender and sharply-pointed two-edged blade.',
        url: 'https://en.wikipedia.org/wiki/Inigo_Montoya',
        userId: 1
    });

    buttercupGift: GiftInput = new GiftInput({
        title: 'True Love',
        description: 'A lovely sounding phrase',
        url: 'https://en.wikipedia.org/wiki/Inigo_Montoya',
        userId: 1
    });

    dreadPirateRobertsGift: GiftInput = new GiftInput({
        title: 'Zoro Mask',
        description: 'allows Westley to prove Buttercups devotion',
        url: 'https://en.wikipedia.org/wiki/Inigo_Montoya',
        userId: 1
    });

    miracleMaxGift: GiftInput = new GiftInput({
        title: 'healing lozenge',
        description: 'A cure to Westleys paralysis',
        url: 'https://en.wikipedia.org/wiki/Inigo_Montoya',
        userId: 1
    });
}
