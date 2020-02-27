import '../styles/site.scss';

//import Vue from 'vue';

//import Blah from './blah.vue';

//new Vue({
//    render: h => h(Blah)
//}).$mount('#blah');

import { App } from './app';
import { Post } from './blogengine-client.g';

document.addEventListener("DOMContentLoaded", async () => {
    let app = new App.Main();

    await app.deletePosts();

    await app.createAuthor();

    await app.createPosts();

    let posts = await app.getPosts();

    let element = document.getElementById('postList');

    for (let post of posts) {
        let liElement = element.appendChild(document.createElement('li'));
        liElement.textContent = `${post.id} ${post.title} ${post.content}`;
    }

});