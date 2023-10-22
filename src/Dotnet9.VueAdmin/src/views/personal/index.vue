<template>
	<div class="personal layout-pd">
		<el-row>
			<!-- 个人信息 -->
			<el-col :xs="24" :sm="16">
				<el-card shadow="hover" header="个人信息">
					<div class="personal-user">
						<div class="personal-user-left">
							<el-upload
								class="h100 personal-user-left-upload"
								action="/api/file/upload"
								:with-credentials="true"
								accept="image/*"
								:limit="1"
								:on-success="onSuccess"
								:show-file-list="false"
							>
								<el-avatar v-if="model.info.avatar" :size="100" :src="model.info.avatar"></el-avatar>
								<el-avatar :size="100" v-else>{{ model.info.name }}</el-avatar>
							</el-upload>
						</div>
						<div class="personal-user-right">
							<el-row>
								<el-col :span="24" class="personal-title mb18"
									>{{ currentTime }}，{{ model.info.account }}，生活变的再糟糕，也不妨碍我变得更好！
								</el-col>
								<el-col :span="24">
									<el-row>
										<el-col :xs="24" :sm="8" class="personal-item mb6">
											<div class="personal-item-label">登录名：</div>
											<div class="personal-item-value">{{ model.info.account }}</div>
										</el-col>
										<el-col :xs="24" :sm="16" class="personal-item mb6">
											<div class="personal-item-label">姓名：</div>
											<div class="personal-item-value">{{ model.info.name ?? '无' }}</div>
										</el-col>
									</el-row>
								</el-col>
								<el-col :span="24">
									<el-row>
										<el-col :xs="24" :sm="8" class="personal-item mb6">
											<div class="personal-item-label">昵称：</div>
											<div class="personal-item-value">{{ model.info.nickName }}</div>
										</el-col>
										<el-col :xs="24" :sm="16" class="personal-item mb6">
											<div class="personal-item-label">部门：</div>
											<div class="personal-item-value">{{ model.info.orgName ?? '无' }}</div>
										</el-col>
									</el-row>
								</el-col>
								<el-col :span="24">
									<el-row>
										<el-col :xs="24" :sm="8" class="personal-item mb6">
											<div class="personal-item-label">登录IP：</div>
											<div class="personal-item-value">{{ model.info.lastLoginIp }}</div>
										</el-col>
										<el-col :xs="24" :sm="16" class="personal-item mb6">
											<div class="personal-item-label">登录地址：</div>
											<div class="personal-item-value">{{ model.info.lastLoginAddress ?? '无' }}</div>
										</el-col>
									</el-row>
								</el-col>
							</el-row>
						</div>
					</div>
				</el-card>
			</el-col>

			<!-- 修改密码 -->
			<el-col :xs="24" :sm="8" class="pl15 personal-info">
				<el-card shadow="hover">
					<template #header>
						<span>修改密码</span>
					</template>
					<div class="personal-info-box">
						<el-form ref="pwdFormRef" :model="model.pwd" :rules="pwdRules" size="default">
							<er-row>
								<el-col class="mb15">
									<el-form-item label="原密码" prop="originalPwd">
										<el-input
											v-model="model.pwd.originalPwd"
											maxlength="16"
											type="password"
											placeholder="请输入原密码"
											minlength="6"
											show-password
										></el-input>
									</el-form-item>
								</el-col>
								<el-col class="mb15">
									<el-form-item label="新密码" prop="password">
										<el-input
											maxlength="16"
											v-model="model.pwd.password"
											type="password"
											minlength="6"
											placeholder="请输入新密码"
											show-password
										></el-input>
									</el-form-item>
								</el-col>
								<el-col style="display: flex; justify-content: flex-end">
									<el-button type="primary" @click="onChangePwd">提交</el-button>
								</el-col>
							</er-row>
						</el-form>
					</div>
				</el-card>
			</el-col>

			<!-- 营销推荐 -->
			<!-- <el-col :span="24" style="display: none">
				<el-card shadow="hover" class="mt15" header="营销推荐">
					<el-row :gutter="15" class="personal-recommend-row">
						<el-col :sm="6" v-for="(v, k) in state.recommendList" :key="k" class="personal-recommend-col">
							<div class="personal-recommend" :style="{ 'background-color': v.bg }">
								<SvgIcon :name="v.icon" :size="70" :style="{ color: v.iconColor }" />
								<div class="personal-recommend-auto">
									<div>{{ v.title }}</div>
									<div class="personal-recommend-msg">{{ v.msg }}</div>
								</div>
							</div>
						</el-col>
					</el-row>
				</el-card>
			</el-col> -->

			<!-- 更新信息 -->
			<el-col :span="24">
				<el-card shadow="hover" class="mt15 personal-edit" header="更新信息">
					<div class="personal-edit-title">基本信息</div>
					<el-form ref="userInfoFormRef" :rules="userInfoRules" :model="state.user" size="default" label-width="60px" class="mt35 mb35">
						<el-row :gutter="35">
							<el-col :xs="24" :sm="12" :md="8" :lg="6" :xl="4" class="mb20">
								<el-form-item label="姓名" prop="name">
									<el-input v-model="state.user.name" placeholder="请输入姓名" maxlength="16" clearable></el-input>
								</el-form-item>
							</el-col>
							<el-col :xs="24" :sm="12" :md="8" :lg="6" :xl="4" class="mb20">
								<el-form-item label="昵称" prop="nickName">
									<el-input v-model="state.user.nickName" placeholder="请输入昵称" maxlength="32" clearable></el-input>
								</el-form-item>
							</el-col>
							<el-col :xs="24" :sm="12" :md="8" :lg="6" :xl="4" class="mb20">
								<el-form-item label="邮箱" prop="email">
									<el-input v-model="state.user.email" placeholder="请输入邮箱" maxlength="64" clearable></el-input>
								</el-form-item>
							</el-col>
							<el-col :xs="24" :sm="12" :md="8" :lg="6" :xl="4" class="mb20">
								<el-form-item label="生日" prop="birthday">
									<el-date-picker
										v-model="state.user.birthday"
										format="YYYY-MM-DD"
										value-format="YYYY-MM-DD"
										placeholder="请选择出生日期"
										clearable
									></el-date-picker>
								</el-form-item>
							</el-col>
							<el-col :xs="24" :sm="12" :md="8" :lg="6" :xl="4" class="mb20">
								<el-form-item label="性别" prop="gender">
									<el-select v-model="state.user.gender" placeholder="请选择性别" clearable class="w100">
										<el-option label="男" :value="0"></el-option>
										<el-option label="女" :value="1"></el-option>
									</el-select>
								</el-form-item>
							</el-col>
							<el-col :xs="24" :sm="12" :md="8" :lg="6" :xl="4" class="mb20">
								<el-form-item label="手机" prop="mobile">
									<el-input v-model="state.user.mobile" maxlength="11" placeholder="请输入手机" clearable></el-input>
								</el-form-item>
							</el-col>
							<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
								<el-form-item>
									<el-button type="primary" @click="onSave">
										<el-icon>
											<ele-Position />
										</el-icon>
										更新个人信息
									</el-button>
								</el-form-item>
							</el-col>
						</el-row>
					</el-form>
					<!-- <div class="personal-edit-title mb15">账号安全</div>
					<div class="personal-edit-safe-box">
						<div class="personal-edit-safe-item">
							<div class="personal-edit-safe-item-left">
								<div class="personal-edit-safe-item-left-label">账户密码</div>
								<div class="personal-edit-safe-item-left-value">当前密码强度：强</div>
							</div>
							<div class="personal-edit-safe-item-right">
								<el-button text type="primary">立即修改</el-button>
							</div>
						</div>
					</div>
					<div class="personal-edit-safe-box">
						<div class="personal-edit-safe-item">
							<div class="personal-edit-safe-item-left">
								<div class="personal-edit-safe-item-left-label">密保手机</div>
								<div class="personal-edit-safe-item-left-value">已绑定手机：132****4108</div>
							</div>
							<div class="personal-edit-safe-item-right">
								<el-button text type="primary">立即修改</el-button>
							</div>
						</div>
					</div> -->
					<!-- <div class="personal-edit-safe-box">
						<div class="personal-edit-safe-item">
							<div class="personal-edit-safe-item-left">
								<div class="personal-edit-safe-item-left-label">密保问题</div>
								<div class="personal-edit-safe-item-left-value">已设置密保问题，账号安全大幅度提升</div>
							</div>
							<div class="personal-edit-safe-item-right">
								<el-button text type="primary">立即设置</el-button>
							</div>
						</div>
					</div>
					<div class="personal-edit-safe-box">
						<div class="personal-edit-safe-item">
							<div class="personal-edit-safe-item-left">
								<div class="personal-edit-safe-item-left-label">绑定QQ</div>
								<div class="personal-edit-safe-item-left-value">已绑定QQ：110****566</div>
							</div>
							<div class="personal-edit-safe-item-right">
								<el-button text type="primary">立即设置</el-button>
							</div>
						</div>
					</div> -->
				</el-card>
			</el-col>
		</el-row>
	</div>
</template>

<script setup lang="ts" name="personal">
import { ref, reactive, computed, onMounted } from 'vue';
import { formatAxis } from '/@/utils/formatTime';
import SysUserApi from '/@/api/SysUserApi';
import type { ChangePasswordOutput, SysUserInfoOutput, UpdateCurrentUserInput } from '/@/api/models';
import { useUserInfo } from '/@/stores/userInfo';
import { type FormRules, type FormInstance, ElMessage } from 'element-plus';
const pwdRules = reactive<FormRules>({
	originalPwd: [{ required: true, message: '请输入原密码' }],
	password: [{ required: true, message: '请输入新密码' }],
});
const userInfoRules = reactive<FormRules>({
	name: [{ required: true, message: '请输入姓名' }],
	nickName: [{ required: true, message: '请输入昵称' }],
	email: [{ required: true, message: '请输入邮箱' }],
	birthday: [{ required: true, message: '请选择出生日期' }],
	gender: [{ required: true, message: '请选择性别' }],
	mobile: [{ required: true, message: '请输入手机' }],
});
const pwdFormRef = ref<FormInstance>();
const userInfoFormRef = ref<FormInstance>();
const storeUser = useUserInfo();
// 定义变量内容
const state = reactive({
	user: {} as UpdateCurrentUserInput,
});

const model = reactive({
	info: {} as SysUserInfoOutput, //当前用户信息
	pwd: {} as ChangePasswordOutput,
});
const onChangePwd = () => {
	pwdFormRef.value?.validate(async (valid) => {
		if (valid) {
			const { succeeded } = await SysUserApi.changePassword(model.pwd);
			if (succeeded) {
				ElMessage.success('修改成功，下次登录请使用新密码！');
				pwdFormRef.value?.resetFields();
			}
		}
	});
};
// 修改个人信息
const onSave = async () => {
	userInfoFormRef.value?.validate(async (valid) => {
		if (valid) {
			const { succeeded } = await SysUserApi.updateCurrentUser(state.user);
			if (succeeded) {
				ElMessage.success('修改成功');
				await storeUser.getUserInfo();
			}
		}
	});
};
// 上传头像回调
const onSuccess = async (response: any) => {
	const { succeeded } = await SysUserApi.setAvatar(response[0].url);
	if (succeeded) {
		model.info.avatar = response[0].url;
		await storeUser.getUserInfo();
		ElMessage.success('修改头像成功');
	}
};
onMounted(async () => {
	const { data } = await SysUserApi.getCurrentUserInfo();
	model.info = data!;
	state.user.birthday = data?.birthday;
	state.user.name = data!.name!;
	state.user.email = data?.email;
	state.user.mobile = data?.mobile;
	state.user.gender = data?.gender;
	state.user.nickName = data?.nickName;
});

// 当前时间提示语
const currentTime = computed(() => {
	return formatAxis(new Date());
});
</script>

<style scoped lang="scss">
@import '../../theme/mixins/index.scss';
.personal {
	.personal-user {
		height: 130px;
		display: flex;
		align-items: center;
		.personal-user-left {
			width: 100px;
			height: 130px;
			border-radius: 3px;
			:deep(.el-upload) {
				height: 100%;
			}
			.personal-user-left-upload {
				img {
					width: 100%;
					height: 100%;
					border-radius: 3px;
				}
				&:hover {
					img {
						animation: logoAnimation 0.3s ease-in-out;
					}
				}
			}
		}
		.personal-user-right {
			flex: 1;
			padding: 0 15px;
			.personal-title {
				font-size: 18px;
				@include text-ellipsis(1);
			}
			.personal-item {
				display: flex;
				align-items: center;
				font-size: 13px;
				.personal-item-label {
					color: var(--el-text-color-secondary);
					@include text-ellipsis(1);
				}
				.personal-item-value {
					@include text-ellipsis(1);
				}
			}
		}
	}
	.personal-info {
		.personal-info-more {
			float: right;
			color: var(--el-text-color-secondary);
			font-size: 13px;
			&:hover {
				color: var(--el-color-primary);
				cursor: pointer;
			}
		}
		.personal-info-box {
			height: 130px;
			overflow: hidden;
			.personal-info-ul {
				list-style: none;
				.personal-info-li {
					font-size: 13px;
					padding-bottom: 10px;
					.personal-info-li-title {
						display: inline-block;
						@include text-ellipsis(1);
						color: var(--el-text-color-secondary);
						text-decoration: none;
					}
					& a:hover {
						color: var(--el-color-primary);
						cursor: pointer;
					}
				}
			}
		}
	}
	.personal-recommend-row {
		.personal-recommend-col {
			.personal-recommend {
				position: relative;
				height: 100px;
				border-radius: 3px;
				overflow: hidden;
				cursor: pointer;
				&:hover {
					i {
						right: 0px !important;
						bottom: 0px !important;
						transition: all ease 0.3s;
					}
				}
				i {
					position: absolute;
					right: -10px;
					bottom: -10px;
					font-size: 70px;
					transform: rotate(-30deg);
					transition: all ease 0.3s;
				}
				.personal-recommend-auto {
					padding: 15px;
					position: absolute;
					left: 0;
					top: 5%;
					color: var(--next-color-white);
					.personal-recommend-msg {
						font-size: 12px;
						margin-top: 10px;
					}
				}
			}
		}
	}
	.personal-edit {
		.personal-edit-title {
			position: relative;
			padding-left: 10px;
			color: var(--el-text-color-regular);
			&::after {
				content: '';
				width: 2px;
				height: 10px;
				position: absolute;
				left: 0;
				top: 50%;
				transform: translateY(-50%);
				background: var(--el-color-primary);
			}
		}
		.personal-edit-safe-box {
			border-bottom: 1px solid var(--el-border-color-light, #ebeef5);
			padding: 15px 0;
			.personal-edit-safe-item {
				width: 100%;
				display: flex;
				align-items: center;
				justify-content: space-between;
				.personal-edit-safe-item-left {
					flex: 1;
					overflow: hidden;
					.personal-edit-safe-item-left-label {
						color: var(--el-text-color-regular);
						margin-bottom: 5px;
					}
					.personal-edit-safe-item-left-value {
						color: var(--el-text-color-secondary);
						@include text-ellipsis(1);
						margin-right: 15px;
					}
				}
			}
			&:last-of-type {
				padding-bottom: 0;
				border-bottom: none;
			}
		}
	}
}
</style>
