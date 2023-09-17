<template>
    <ElCard>
        <template #header>
            测试表格
        </template>
        <SimpleTable url="/admin/account/getlist">
            <ElTableColumn label="Id" prop="id"></ElTableColumn>
            <ElTableColumn label="账号" prop="userName"></ElTableColumn>
            <ElTableColumn label="邮箱" prop="email"></ElTableColumn>
            <ElTableColumn label="最后登录时间" prop="lastLoginTime"></ElTableColumn>
            <ElTableColumn label="创建时间" prop="createTime"></ElTableColumn>
            <ElTableColumn label="锁定时间">
                <template #default="scope">
                    <label v-if="scope.row.isLocked">{{ scope.row.lockedTime }}</label>
                </template>
            </ElTableColumn>
            <ElTableColumn label="角色"></ElTableColumn>
            <ElTableColumn label="状态">
                <template #default="scope">
                    <ElTag v-if="scope.row.isLocked" type="danger">锁定</ElTag>
                    <ElTag v-else type="success">正常</ElTag>
                </template>
            </ElTableColumn>
            <ElTableColumn label="操作">
                <template #default="scope">
                    <ElDropdown>

                        <ElButton size="small">操作</ElButton>
                        <template #dropdown>
                            <ElDropdownItem>
                                修改资料
                            </ElDropdownItem>
                            <!-- <ElDropdownItem v-if="scope.row.isLocked" @click="forbidLogin(scope.row.id)">
                                解封账号
                            </ElDropdownItem>
                            <ElDropdownItem v-if="!scope.row.isLocked" @click="forbidLogin(scope.row.id)">
                                禁用账号
                            </ElDropdownItem> -->
                            <ElDropdownItem>
                                修改密码
                            </ElDropdownItem>
                        </template>
                    </ElDropdown>
                </template>
            </ElTableColumn>
        </SimpleTable>
    </ElCard>
</template>

<script setup lang="ts">
import SimpleTable from '@/components/SimpleTable.vue';
</script>