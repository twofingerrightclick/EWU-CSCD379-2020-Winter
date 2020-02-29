import {
    IGiftClient,
    GiftClient,
    IUserClient,
    UserClient,
    Gift,
    User,
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
    document.getElementById("cancelButtonDiv").style.visibility = "hidden";
    document.getElementById("optionalColumn").innerText = "";
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
           this.appendToTable(gift, tableBody,null);
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
        if (users.length<2) {
            var user1 = await this.userClient.post(this.userDreadPirate);
            var user2 =await this.userClient.post(this.userInigo);
          
        }

     
        this.buttercupGift.userId = user1.id;
        this.imGift.userId = user2.id;
        this.miracleMaxGift.userId = user2.id;
        this.dreadPirateRobertsGift.userId = user1.id;
        this.iocaneGift.userId = user1.id;
 
        
            await this.giftClient.post(this.buttercupGift);
            await this.giftClient.post(this.imGift);
            await this.giftClient.post(this.miracleMaxGift);
            await this.giftClient.post(this.dreadPirateRobertsGift);
            await this.giftClient.post(this.iocaneGift);
   
    }


    async renderGifts() {

        var gifts = await this.getAllGifts();

        if (gifts.length < 5) {
            await this.deleteAllGifts();
            console.log("Created New gifts");
            await this.populateGifts();
        }
      

        console.log(`number of gifts: ${gifts.length}`);

        var tableBody = document.getElementById("tableBody");
       
        gifts.forEach(gift => {
            this.appendToTable(gift, tableBody,null);
        })
    

    }

    appendToTable(gift: Gift, tableBody: HTMLElement, user:User) {
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

        if (user) {
            let userName = document.createElement("td");
            userName.textContent = `${user.firstName} ${user.lastName}`
            tableRow.append(userName);
        }
       

        tableBody.append(tableRow);
    }


    async searchGifts() {
    
        var searchInput: string = (<HTMLInputElement>document.getElementById("input")).value;

        if (!(searchInput && searchInput.length > 0)) { console.log("invalid search term"); return; }


        document.getElementById("cancelButtonDiv").style.visibility = "visible";
       
        var users = await this.getAllUsers();
        var gifts = await this.getAllGifts();
      

        document.getElementById("results").innerText = "Results";
        var tableBody = document.getElementById("tableBody");
        tableBody.innerHTML = "";

      
        var match: boolean = false;
        users.forEach(user => {

            if (user.firstName.toLowerCase().includes(searchInput) || user.lastName.toLowerCase().includes(searchInput)) {
                match = true;

                console.log(`match and user giftcount: ${user.gifts.length}`);

                document.getElementById("optionalColumn").innerText=("User Name");


                gifts.forEach(gift => {
                    if (user.id == gift.userId)

                        this.appendToTable(gift, tableBody, user);

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

    userDreadPirate: UserInput = new UserInput({
        firstName: 'Dread Pirate',
        lastName: 'Roberts'
    });

    userInigo: UserInput = new UserInput({
        firstName: 'Inigo',
        lastName: 'Montoya'
    });


  
         imGift:GiftInput = new GiftInput({
            title: 'spanish rapier',
            description: 'A Rapier is type of sword with a slender and sharply-pointed two-edged blade.',
            url: 'https://en.wikipedia.org/wiki/Inigo_Montoya',
            userId: 1
        });

buttercupGift:GiftInput = new GiftInput({
    title: 'True Love',
    description: 'A lovely sounding phrase easily misheard as "to blave," which means "to bluff" ',
    url: 'https://en.wikipedia.org/wiki/Inigo_Montoya',
    userId: 1
});

iocaneGift:GiftInput = new GiftInput({
    title: 'Iocane Powder',
    description: "Iocane powder is noted as being one of the deadliest poisons known to man.  It has no odor, no taste, and will dissolve instantly when poured into liquid.",
    url: 'https://en.wikipedia.org/wiki/Inigo_Montoya',
    userId: 1
});


dreadPirateRobertsGift:GiftInput = new GiftInput({
    title: 'Zoro Mask',
    description: 'allows Westley to test Buttercups devotion',
    url: 'https://en.wikipedia.org/wiki/Inigo_Montoya',
    userId: 1
});

miracleMaxGift:GiftInput = new GiftInput({
    title: 'healing lozenge',
    description: 'A cure to Westleys paralysis',
    url: 'https://en.wikipedia.org/wiki/Inigo_Montoya',
    userId: 1
});



    
}
