import { App } from "vue";

import FullScreen from "./FullScreen.vue";
import BreadCrumb from "./BreadCrumb.vue"
import RouteTab from "./RouteTab.vue"
import Tab from "../views/dashboard/Tab.vue"
import LeftMenuLayout from "./LeftMenuLayout.vue"
import TagBox from "./TagBox.vue"
import CropperBox from "./CropperBox.vue";

export default {
    install(app: App<Element>) {
        app.component(FullScreen.name, FullScreen)
        app.component(BreadCrumb.name, BreadCrumb)
        app.component(RouteTab.name, RouteTab)
        app.component(Tab.name, Tab)
        app.component(LeftMenuLayout.name, LeftMenuLayout)
        app.component(TagBox.name,TagBox)
        app.component(CropperBox.name,CropperBox)
    }
}