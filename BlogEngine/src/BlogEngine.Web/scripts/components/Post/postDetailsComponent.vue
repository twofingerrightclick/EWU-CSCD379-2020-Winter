<template>
    <div class="modal is-active">
        <div class="modal-background"></div>
        <div class="modal-content">
            <div class="field">
                <label class="label has-text-white">Title</label>
                <div class="control">
                    <input class="input" type="text" v-model="clonedPost.title" />
                </div>
            </div>
            <div class="field">
                <label class="label has-text-white">Content</label>
                <div class="control">
                    <input class="input" type="text" v-model="clonedPost.content" />
                </div>
            </div>
            <div class="field">
                <label class="checkbox has-text-white">Is Published <input type="checkbox" v-model="clonedPost.isPublished" /></label>
            </div>
            <div class="field">
                <label class="label has-text-white">Posted On</label>
                <div class="control">
                    <input class="input" type="text" v-model="clonedPost.postedOn" />
                </div>
            </div>
            <div class="field">
                <label class="label has-text-white">Author</label>
                <div class="select">
                    <select v-model="clonedPost.authorId">
                        <option v-for="author in authors" :value="author.id">
                            {{author.firstName}} {{author.lastName}}
                        </option>
                    </select>
                </div>
            </div>
            <div class="field is-grouped">
                <div class="control">
                    <button id="submit" class="button is-primary" @click.once="save">Submit</button>
                </div>
                <div class="control">
                    <a class="button is-light" @click="cancel">Cancel</a>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { Vue, Component, Prop, Emit } from 'vue-property-decorator'
    import { Post, PostClient, Author, AuthorClient } from '../../blogengine-client.g';
    @Component
    export default class PostDetailsComponent extends Vue {
        @Prop()
        post: Post;

        clonedPost: Post = <Post>{};
        authors: Author[] = null;

        constructor() {
            super();
        }

        async mounted() {
             // get list of authors for dropdown
            let authorClient = new AuthorClient();
            this.authors = await authorClient.getAll();
            let tempPost = { ...this.post };
            this.clonedPost = <Post>tempPost;
        }

        @Emit('post-saved')
        async save() {
            let postClient = new PostClient();
            this.clonedPost.postedOn = new Date(this.clonedPost.postedOn);
            if (this.clonedPost.id > 0) {
                await postClient.put(this.clonedPost.id, this.clonedPost);
            }
            else {
                await postClient.post(this.clonedPost);
            }
        }

        @Emit('post-saved')
        cancel() {

        }
    }
</script>