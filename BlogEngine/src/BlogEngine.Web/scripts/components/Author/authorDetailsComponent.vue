<template>
    <div>
        <div class="field">
            <label class="label">First Name</label>
            <div class="control">
                <input class="input" type="text" v-model="clonedAuthor.firstName" />
            </div>
        </div>
        <div class="field">
            <label class="label">Last Name</label>
            <div class="control">
                <input class="input" type="text" v-model="clonedAuthor.lastName" />
            </div>
        </div>
        <div class="field">
            <label class="label">Email Address</label>
            <div class="control">
                <input class="input" type="email" v-model="clonedAuthor.email" />
            </div>
        </div>
        <div class="field is-grouped">
            <div class="control">
                <button id="submit" class="button is-primary" @click.once="saveAuthor">Submit</button>
            </div>
            <div class="control">
                <a class="button is-light" @click="cancelEdit">Cancel</a>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { Vue, Component, Prop, Emit } from 'vue-property-decorator'
    import { Author, AuthorClient } from '../../blogengine-client.g';
    @Component
    export default class AuthorDetailsComponent extends Vue {
        @Prop()
        author: Author;
        clonedAuthor: Author = <Author>{};

        constructor() {
            super();
        }

        mounted() {
            let tempAuthor = { ...this.author };
            this.clonedAuthor = <Author>tempAuthor;
        }

        @Emit('author-saved')
        async saveAuthor() {
            let authorClient = new AuthorClient();
            if (this.clonedAuthor.id > 0) {
                await authorClient.put(this.clonedAuthor.id, this.clonedAuthor);
            }
            else {
                await authorClient.post(this.clonedAuthor);
            }
        }

        @Emit('author-saved')
        cancelEdit() {

        }
    }
</script>