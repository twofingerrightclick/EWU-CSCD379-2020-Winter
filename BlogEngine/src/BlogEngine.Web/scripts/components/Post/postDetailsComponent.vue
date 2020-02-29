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
                    <date-picker></date-picker>
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
    import { Post, PostClient } from '../../blogengine-client.g';
    import DatePicker from 'vuejs-datepicker';
    @Component({
        components: {
            DatePicker
        }
    })
    export default class PostDetailsComponent extends Vue {
        @Prop()
        post: Post;
        clonedPost: Post = <Post>{};

        constructor() {
            super();
        }

        mounted() {
            let tempPost = { ...this.post };
            this.clonedPost = <Post>tempPost;
        }

        @Emit('post-saved')
        async save() {
            let postClient = new PostClient();
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