<template>
  <router-link to="/talks" class="swiper-container">
    <v-icon size="20" color="#4c4948">mdi-chat-outline</v-icon>
    <div
      :style="{ height: height * lineNum + 'px' }"
      class="rollScreen_container"
      id="rollScreen_container"
    >
      <ul
        class="rollScreen_list"
        :style="{ transform }"
        :class="{ rollScreen_list_unanim: num === 0 }"
      >
        <li
          class="rollScreen_once"
          v-for="(item, index) in list"
          :key="index"
          :style="{ height: height + 'px' }"
        >
          <span class="item" v-html="item" />
        </li>
        <li
          class="rollScreen_once"
          v-for="(item, index) in list"
          :key="'k' + index"
          :style="{ height: height + 'px' }"
        >
          <span class="item" v-html="item" />
        </li>
      </ul>
    </div>
    <v-icon size="20" color="#4c4948" class="arrow">
      mdi-chevron-double-right
    </v-icon>
  </router-link>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from "vue";
const props = defineProps({
  height: {
    default: 25,
    type: Number,
  },
  lineNum: {
    default: 1,
    type: Number,
  },
  list: {
    type: Array<string>,
    default: [],
  },
});
const num = ref(0);
const transform = computed(() => {
  return `translateY(-${num.value * props.height}px)`;
});
onMounted(() => {
  setInterval(() => {
    if (num.value != props.list!.length) {
      num.value++;
    } else {
      num.value = 0;
    }
  }, 3000);
});
</script>

<style>
.swiper-container {
  padding: 0.6rem 1rem;
  display: flex;
  align-items: center;
  justify-content: space-between;
  font-size: 15px;
}
.rollScreen_container {
  width: 100%;
  line-height: 25px;
  text-align: center;
  display: inline-block;
  position: relative;
  overflow: hidden;
}
.item {
  width: 100%;
  overflow: hidden;
  white-space: nowrap;
  text-overflow: ellipsis;
  transition: all 0.3s;
}
.rollScreen_list:hover .item {
  color: #8e8cd8;
}
.rollScreen_list {
  transition: 1s linear;
}
.rollScreen_list_unanim {
  transition: none;
}
.arrow {
  animation: 1s passing infinite;
}
@keyframes passing {
  0% {
    transform: translateX(-50%);
    opacity: 0;
  }
  50% {
    transform: translateX(0);
    opacity: 1;
  }
  100% {
    transform: translateX(50%);
    opacity: 0;
  }
}
</style>
