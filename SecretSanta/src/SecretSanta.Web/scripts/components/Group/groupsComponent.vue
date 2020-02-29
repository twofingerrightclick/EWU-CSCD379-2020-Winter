<template>
    <div>
        <h2>Groups</h2>
        <button class="button is-secondary" @click="create()">Create New</button>
        <table class="table">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Title</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="group in groups" :key="group.id">
                    <td>{{group.id}}</td>
                    <td>{{group.title}}</td>
                    <td>
                        <button class="button is-primary" @click="Edit(group)">Edit</button>
                        <button class="button" @click="Delete(group)">Delete</button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>
<script lang="ts">
    import { Vue, Component } from 'vue-property-decorator';
    import { Group, GroupClient } from '../../secretsanta-client';
    @Component
    export default class GroupsComponent extends Vue {
        groups: Group[] = null;
        selectedGroup: Group = null;
        groupClient: GroupClient;

        constructor() {
            super();
            this.groupClient = new GroupClient();
        }

        async mounted() {
            this.groups = await this.groupClient.getAll()
        }

        create() {
            this.selectedGroup = <Group>{};
        }

        async Edit(group: Group) {

        }

        async Delete(group: Group) {

        }
    }
</script>