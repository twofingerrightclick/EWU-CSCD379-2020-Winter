import {
    IGiftClient,
    GiftClient,
    IUserClient,
    UserClient,
    Gift,
    GiftInput,
    UserInput
} from "./secret-santa-api.client";


let searchButton = document.getElementById("searchButton");
searchButton.addEventListener("click", (e: Event) => searchByTitle());

let cancelButton = document.getElementById("cancelButton");
cancelButton.addEventListener("click", (e: Event) => clearSearch());


function searchByTitle() {
    (new GiftList().searchGifts());
}


function clearSearch() {
    document.getElementById("results").innerText = "";
    (<HTMLInputElement>document.getElementById("input")).value = "";
    (new GiftList().reloadList());
}








export class GiftList {

    async reloadList() {

        var gifts = await this.getAllGifts();


       var tableBody = document.getElementById("tableBody");
       tableBody.innerHTML = "";
        for (let gift of gifts) {
            console.log("apending");
           this.appendToTable(gift, tableBody);
       }
    }



    async deleteAllGifts() {
        var gifts = await this.getAllGifts();
        console.log("In render gifts");

       for (let gift  of gifts ) {
           await this.giftClient.delete(gift.id);

        }

    }

    async populateGifts() {

        var users = await this.getAllUsers();
        //trying to figure out how to use the promise<User> on the get(int id) versus getAll
        if (users.length<1) {
            await this.userClient.post(this.userThePrincessBride);
            console.log("Created New User 1");
        }
        await this.giftClient.post(this.buttercupGift);
        await this.giftClient.post(this.imGift);
        await this.giftClient.post(this.miracleMaxGift);
        await this.giftClient.post(this.dreadPirateRobertsGift);
        await this.giftClient.post(this.iocaneGift);

    }


    async renderGifts() {
        await this.deleteAllGifts();
 
        await this.populateGifts();


        var gifts = await this.getAllGifts();
        console.log(`number of gifts: ${gifts.length}`);

        var tableBody = document.getElementById("tableBody");
       
        gifts.forEach(gift => {
            this.appendToTable(gift, tableBody);
        })
    

    }
    appendToTable(gift: Gift, tableBody: HTMLElement) {
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
    }


    async searchGifts() {
    

       
        var users = await this.getAllUsers();
      

        document.getElementById("results").innerText = "Results";
        var tableBody = document.getElementById("tableBody");
        tableBody.innerHTML = "";

        var searchInput: string = (<HTMLInputElement>document.getElementById("input")).value;
        var match: boolean = false;
        users.forEach(user => {

            if (user.firstName.toLowerCase().includes(searchInput) || user.lastName.toLowerCase().includes(searchInput)) {
                match = true;

                console.log("match");

                user.gifts.forEach(gift => {

              
                    this.appendToTable(gift, tableBody);

                })
            
            }

        })

        if (!match) {
            document.getElementById("tableBody").innerHTML = "<h1>no items found<h1>";
        }



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

    async getAllUsers() {
        var users = await this.userClient.getAll();
        return users;
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
        description: 'A lovely sounding phrase easily misheard as "to blave," which means "to bluff" ',
        url: 'https://en.wikipedia.org/wiki/Inigo_Montoya',
        userId: 1
    });

    iocaneGift: GiftInput = new GiftInput({
        title: 'Iocane Powder',
        description: "Iocane powder is noted as being one of the deadliest poisons known to man.  It has no odor, no taste, and will dissolve instantly when poured into liquid.",
        url: 'https://en.wikipedia.org/wiki/Inigo_Montoya',
        userId: 1
    });


    dreadPirateRobertsGift: GiftInput = new GiftInput({
        title: 'Zoro Mask',
        description: 'allows Westley to test Buttercups devotion',
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
