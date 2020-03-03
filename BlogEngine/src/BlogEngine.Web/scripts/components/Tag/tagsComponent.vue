<template>
    <div>
        <button class="button is-secondary" @click="createAuthor()">Create New</button>
        <table class="table">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Email Address</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="author in authors" :id="author.id">
                    <td>{{author.id}}</td>
                    <td>{{author.firstName}}</td>
                    <td>{{author.lastName}}</td>
                    <td>{{author.email}}</td>
                    <td>
                        <button class="button is-primary" @click='setAuthor(author)'>Edit</button>
                        <button class="button" @click='deleteAuthor(author)'>Delete</button>
                    </td>
                </tr>
            </tbody>
        </table>
        <author-details-component v-if="selectedAuthor != null"
                                  :author="selectedAuthor"
                                  @author-saved="refreshAuthors()"></author-details-component>
    </div>
</template>
<script lang="ts">
    import { Vue, Component } from 'vue-property-decorator'
    import { Author, AuthorClient } from '../../blogengine-client.g';
    import AuthorDetailsComponent from './authorDetailsComponent.vue';
    @Component({
        components: {
            AuthorDetailsComponent
        }
    })
    export default class AuthorsComponent extends Vue {
        authors: Author[] = null;
        selectedAuthor: Author = null;

        async loadAuthors() {
            let authorClient = new AuthorClient();
            this.authors = await authorClient.getAll();
        }

        createAuthor() {
            this.selectedAuthor = <Author>{};
        }

        async mounted() {
                    await this.loadAuthors();
        }

        setAuthor(author: Author) {
                    this.selectedAuthor = author;
        }

        async refreshAuthors() {
                    this.selectedAuthor = null;
            await this.loadAuthors();
        }

        async deleteAuthor(author: Author) {
                    let authorClient = new AuthorClient();
                if (confirm(`Are you sure you want to delete ${author.firstName} ${author.lastName}`)) {
                    await authorClient.delete(author.id);
                }

                await this.refreshAuthors();
        }
    }
</script>