import '../styles/site.scss';

import Vue from 'vue';

import Blah from './blah.vue';

new Vue({
    render: h => h(Blah)
}).$mount('#blah');