<template>
    <ElCard>
        <template #header>
            <div class="card-header">
                <span class="title">后台账号</span>
            </div>
        </template>
        <ElRow>
            <ElCol>
                <ElSpace>
                    <ElButton type="primary">添加账号</ElButton>
                </ElSpace>
            </ElCol>
        </ElRow>
        <SimpleTable url="/admin/Account/GetList" ref="table">
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
                            <ElDropdownItem v-if="scope.row.isLocked" @click="forbidLogin(scope.row.id)">
                                解封账号
                            </ElDropdownItem>
                            <ElDropdownItem v-if="!scope.row.isLocked" @click="forbidLogin(scope.row.id)">
                                禁用账号
                            </ElDropdownItem>
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

<script lang="ts" setup>


import { AccountService } from '@/shared/service'
import { ElMessage } from 'element-plus';
import SimpleTable from '@/components/SimpleTable.vue';
import { ref } from 'vue';


const table = ref<InstanceType<typeof SimpleTable>>();


const forbidLogin = (id: string) => {
    AccountService.accountForbidLogin({
        accountId: id
    }).then(() => {
        ElMessage.success('操作成功')
        table.value?.loadData();
    })
}


</script>