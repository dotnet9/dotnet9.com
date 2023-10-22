import { createApp } from 'vue';
import pinia from '/@/stores/index';
import App from '/@/App.vue';
import router from '/@/router';
import { directive } from '/@/directive/index';
import { i18n } from '/@/i18n/index';
import other from '/@/utils/other';
import VForm3 from 'vform3-builds';

import ElementPlus from 'element-plus';
import '/@/theme/index.scss';
import 'vform3-builds/dist/designer.style.css'; //引入VForm3样式
import VueGridLayout from 'vue-grid-layout';

const app = createApp(App);
directive(app);
other.elSvg(app);

app.use(pinia).use(router).use(ElementPlus).use(i18n).use(VueGridLayout).use(VForm3).mount('#app');
