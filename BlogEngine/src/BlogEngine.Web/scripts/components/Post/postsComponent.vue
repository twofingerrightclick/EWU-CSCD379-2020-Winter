<template>
    <div>
        <button class="button is-secondary" @click="createPost()">Create New</button>
        <table class="table">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Title</th>
                    <th>Is Published</th>
                    <th>Posted On</th>
                    <th>Author</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="post in posts" :id="post.id">
                    <td>{{post.id}}</td>
                    <td>{{post.title}}</td>
                    <td>{{post.isPublished}}</td>
                    <td>{{post.postedOn}}</td>
                    <td>{{post.authorId}}</td>
                    <td>
                        <button class="button is-primary" @click='setPost(post)'>Edit</button>
                        <button class="button" @click='deletePost(post)'>Delete</button>
                    </td>
                </tr>
            </tbody>
        </table>
        <post-details-component v-if="selectedPost != null"
                                  :post="selectedPost"
                                  @post-saved="refreshPosts()"></post-details-component>
    </div>
</template>
<script lang="ts">
    import { Vue, Component } from 'vue-property-decorator'
    import { Post, PostClient } from '../../blogengine-client.g';
    import PostDetailsComponent from './postDetailsComponent.vue';
    @Component({
        components: {
            PostDetailsComponent
        }
    })
    export default class PostsComponent extends Vue {
        posts: Post[] = null;
        selectedPost: Post = null;

        async loadPosts() {
            let postClient = new PostClient();
            this.posts = await postClient.getAll();
        }

        createPost() {
            this.selectedPost = <Post>{};
        }

        async mounted() {
            await this.loadPosts();
        }

        setPost(post: Post) {
            this.selectedPost = post;
        }

        async refreshPosts() {
            this.selectedPost = null;
            await this.loadPosts();
        }

        async deletePost(post: Post) {
            let postClient = new PostClient();
            if (confirm(`Are you sure you want to delete ${post.title}`)) {
                await postClient.delete(post.id);
            }

            await this.refreshPosts();
        }
    }
</script>