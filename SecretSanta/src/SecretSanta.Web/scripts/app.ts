import { GiftClient, Gift, UserClient, User, GroupClient, Group } from './secretsanta-client.g';


export module App {
    export class Main {
        // Properties
        giftClient: GiftClient;
        userClient: UserClient;
        groupClient: GroupClient;
        createdUser: User;

        // Constructor
        constructor() {
            this.giftClient = new GiftClient('https://localhost:44388');
            this.userClient = new UserClient('https://localhost:44388');
            this.groupClient = new GroupClient('https://localhost:44388');
        }


        // GIFTS
        async createGifts() {
            for (let i = 0; i < 5; i++) {
                let gift = new Gift();
                gift.title = `Title ${i}`;
                gift.description = `Description ${i}`;
                gift.url = `Url ${i}`;
                gift.userId = this.createdUser.id;

                await this.giftClient.post(gift);
            }
        }

        async updateGifts() {

        }

        async deleteGifts() {
            let gifts = await this.getGifts();
            for (let gift of gifts) {
                await this.giftClient.delete(gift.id);
            }
        }

        async getGifts() {
            let gifts = await this.giftClient.getAll();
            return gifts;
        }


        // USERS
        async createUser() {
            let users = await this.userClient.getAll();
            if (users.length > 0) {
                this.createdUser = users[0];
            }
            else {
                this.createdUser = new User();
                this.createdUser.firstName = 'Inigo';
                this.createdUser.lastName = 'Montoya';
                await this.userClient.post(this.createdUser);
            }
        }

        async updateUsers() {

        }

        async deleteUsers() {

        }

        async getUsers() {

        }


        // GROUPS
        async createGroups() {

        }

        async updateGroups() {

        }

        async deleteGroups() {

        }

        async getGroups() {

        }
    }
}