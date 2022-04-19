<template>
    <div class="tool">
        <h3>操作</h3>
        <ul>
            <li v-for="item in 10" :key="item">
                <router-link></router-link>
            </li>
        </ul>
    </div>
</template>

<script>
export default {

}
</script>

<style>
</style>