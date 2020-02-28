import { PostClient, Post, AuthorClient, Author } from './blogengine-client.g';

export module App {
    export class Main {
        postClient: PostClient;
        authorClient: AuthorClient;
        createdAuthor: Author;

        constructor() {
            this.postClient = new PostClient('https://localhost:44317');
            this.authorClient = new AuthorClient('https://localhost:44317');
        }

        async deletePosts() {
            var posts = await this.getPosts();

            for (let post of posts) {
                await this.postClient.delete(post.id);
            }
        }

        async createPosts() {
            for (let i = 0; i < 5; i++) {
                let post = new Post();
                post.title = `Title ${i}`;
                post.content = `Content ${i}`;
                post.authorId = this.createdAuthor.id;

                await this.postClient.post(post);
            }
        }

        async getPosts() {
            let posts = await this.postClient.getAll();

            return posts;
        }

        async createAuthor() {
            let authors = await this.authorClient.getAll();

            if (authors.length > 0) {
                this.createdAuthor = authors[0];
            }
            else {
                this.createdAuthor = new Author();
                this.createdAuthor.firstName = 'Inigo';
                this.createdAuthor.lastName = 'Montoya';
                this.createdAuthor.email = 'inigo@montoya.com';
                await this.authorClient.post(this.createdAuthor);
            }
        }
    }
}