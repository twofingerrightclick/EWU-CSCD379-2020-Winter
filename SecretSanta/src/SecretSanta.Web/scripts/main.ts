import '../styles/site.scss';
import Vue from 'vue';
import UsersComponent from './components/User/usersComponent.vue';

document.addEventListener("DOMContentLoaded", async () => {
  

    if (document.getElementById('userList')) {
        new Vue({
            render: h => h(UsersComponent)
        }).$mount('#userList');
    }
});