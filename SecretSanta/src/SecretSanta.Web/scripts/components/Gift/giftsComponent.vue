<template>
    <div>
        <h2>Gifts</h2>
        <button class="button is-secondary" @click="create()">Create New</button>
        <table class="table">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Title</th>
                    <th>Description</th>
                    <th>Url</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="gift in gifts" :key="key.id">
                    <td>{{gift.id}}</td>
                    <td>{{gift.title}}</td>
                    <td>{{gift.description}}</td>
                    <td>{{gift.url}}</td>
                    <td>
                        <button class="button is-primary" @click="Edit(gift)">Edit</button>
                        <button class="button" @click="Delete(gift)">Delete</button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>
<script lang="ts">
    import { Vue, Component } from 'vue-property-decorator';
    import { Gift, GiftClient } from '../../secretsanta-client';
    @Component
    export default class GiftsComponent extends Vue {
        gifts: Gift[] = null;
        selectedGift: Gift = null;
        giftClient: GiftClient;

        constructor() {
            super();
            this.giftClient = new GiftClient();
        }

        async mounted() {
            this.gifts = await this.giftClient.getAll();
        }

        create() {
            this.selectedGift = <Gift>{};
        }

        async Edit(gift: Gift) {

        }

        async Delete(gift: Gift) {

        }
    }
</script>