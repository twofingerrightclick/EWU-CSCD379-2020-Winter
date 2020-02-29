<template>
    <div>
        <h2>Users</h2>
        <button class="button is-secondary" @click="create()">Create New</button>
        <table class="table">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="user in users" :key="user.id">
                    <td>{{user.id}}</td>
                    <td>{{user.firstName}}</td>
                    <td>{{user.lastName}}</td>
                    <td>
                        <button class="button is-primary" @click="Edit(user)">Edit</button>
                        <button class="button" @click="Delete(user)">Delete</button>
                    </td>
                </tr>
            </tbody>
        </table>
        <user-details-component v-if="selectedUser != null"
                                :user="selectedUser"
                                @user-saved="refresh()"></user-details-component>
    </div>
</template>
<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import { User, UserClient } from '../../secretsanta-client';
    import UserDetailsComponent from './userDetailsComponent.vue';

    @Component({
        components: {
            UserDetailsComponent
        }
    })
    export default class UsersComponent extends Vue {
        users: User[] = null;
        selectedUser: User = null;
        userClient: UserClient;

        constructor() {
            super();
            this.userClient = new UserClient();
        }

        async mounted() {
            this.users = await this.userClient.getAll();
        }

        create() {
            this.selectedUser = <User>{};
        }

        async edit(user: User) {

        }

        async delete(user: User) {

        }

        async refresh() {
            this.users = await this.userClient.getAll();
        }
    }
</script>