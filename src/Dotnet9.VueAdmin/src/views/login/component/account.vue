<template>
	<el-form ref="formRef" size="large" class="login-content-form" :rules="rules" :model="state.ruleForm">
		<el-form-item class="login-animation1" prop="account">
			<el-input
				text
				maxlength="32"
				:placeholder="$t('message.account.accountPlaceholder1')"
				v-model="state.ruleForm.account"
				clearable
				autocomplete="off"
			>
				<template #prefix>
					<el-icon class="el-input__icon"><ele-User /></el-icon>
				</template>
			</el-input>
		</el-form-item>
		<el-form-item class="login-animation2" prop="password">
			<el-input
				:type="state.isShowPassword ? 'text' : 'password'"
				:placeholder="$t('message.account.accountPlaceholder2')"
				v-model="state.ruleForm.password"
				autocomplete="off"
				maxlength="18"
			>
				<template #prefix>
					<el-icon class="el-input__icon"><ele-Unlock /></el-icon>
				</template>
				<template #suffix>
					<i
						class="iconfont el-input__icon login-content-password"
						:class="state.isShowPassword ? 'icon-yincangmima' : 'icon-xianshimima'"
						@click="state.isShowPassword = !state.isShowPassword"
					>
					</i>
				</template>
			</el-input>
		</el-form-item>
		<el-form-item class="login-animation3" prop="code">
			<el-col :span="15">
				<el-input
					text
					maxlength="4"
					:placeholder="$t('message.account.accountPlaceholder3')"
					v-model="state.ruleForm.code"
					clearable
					autocomplete="off"
				>
					<template #prefix>
						<el-icon class="el-input__icon"><ele-Position /></el-icon>
					</template>
				</el-input>
			</el-col>
			<el-col :span="1"></el-col>
			<el-col :span="8">
				<el-button class="login-content-code" v-waves @click="onCaptchaChange">
					<img :src="captchaUrl" alt="看不清？点击换一张！" />
				</el-button>
			</el-col>
		</el-form-item>
		<el-form-item class="login-animation4">
			<el-button type="primary" class="login-content-submit" round v-waves @click="onSignIn" :loading="state.loading.signIn">
				<span>{{ $t('message.account.accountBtnText') }}</span>
			</el-button>
		</el-form-item>
	</el-form>
</template>

<script setup lang="ts" name="loginAccount">
import { reactive, computed, ref, onMounted, onBeforeUnmount } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { ElMessage, dayjs } from 'element-plus';
import type { FormInstance, FormRules } from 'element-plus';
import { useI18n } from 'vue-i18n';
import { storeToRefs } from 'pinia';
import { useThemeConfig } from '/@/stores/themeConfig';
import { initFrontEndControlRoutes } from '/@/router/frontEnd';
import { initBackEndControlRoutes } from '/@/router/backEnd';
import { Session } from '/@/utils/storage';
import { formatAxis } from '/@/utils/formatTime';
import { NextLoading } from '/@/utils/loading';
import { login } from '/@/api/AuthApi';
import type { AdminLoginInput } from '/@/api/models';

// 定义变量内容
const { t } = useI18n();
const storesThemeConfig = useThemeConfig();
const { themeConfig } = storeToRefs(storesThemeConfig);
const route = useRoute();
const router = useRouter();
const formRef = ref<FormInstance>();
const rules = reactive<FormRules>({
	account: [
		{ required: true, message: '请输入用户名', trigger: 'blur' },
		{ min: 3, max: 32, message: '用户名限制3-32个字符' },
	],
	password: [
		{
			required: true,
			message: '请输入密码',
			trigger: 'blur',
		},
		{
			min: 6,
			max: 18,
			message: '密码限制6-18个字符',
		},
	],
	code: [
		{
			required: true,
			message: '请输入验证码',
			trigger: 'blur',
		},
	],
});
const state = reactive({
	isShowPassword: false,
	random: new Date().getTime(),
	ruleForm: {
		account: '',
		password: '',
		code: '',
		id: dayjs().valueOf().toString(),
	} as AdminLoginInput,
	loading: {
		signIn: false,
	},
});

// 时间获取
const currentTime = computed(() => {
	return formatAxis(new Date());
});

/**
 * 验证码
 */
const captchaUrl = computed(() => {
	return `/api/auth/captcha?id=${state.ruleForm.id}&r=${state.random}`;
});

const onCaptchaChange = () => {
	state.random = new Date().getTime();
};

// 登录
const onSignIn = async () => {
	formRef.value!.validate(async (valid) => {
		if (valid) {
			state.loading.signIn = true;
			const { statusCode } = await login(state.ruleForm);
			if (statusCode === 200) {
				if (!themeConfig.value.isRequestRoutes) {
					// 前端路由
					const isNoPower = await initFrontEndControlRoutes();
					signInSuccess(isNoPower);
				} else {
					// 后端路由
					const isNoPower = await initBackEndControlRoutes();
					// 执行完 initBackEndControlRoutes，再执行 signInSuccess
					signInSuccess(isNoPower);
				}
			} else {
				onCaptchaChange();
				state.ruleForm.code = '';
			}
			state.loading.signIn = false;
		}
	});
};
// 登录成功后的跳转
const signInSuccess = (isNoPower: boolean | undefined) => {
	if (isNoPower) {
		ElMessage.warning('抱歉，您没有登录权限');
		Session.clear();
	} else {
		// 初始化登录成功时间问候语
		let currentTimeInfo = currentTime.value;
		// 登录成功，跳到转首页
		// 如果是复制粘贴的路径，非首页/登录页，那么登录成功后重定向到对应的路径中
		if (route.query?.redirect) {
			router.push({
				path: <string>route.query?.redirect,
				query: Object.keys(<string>route.query?.params).length > 0 ? JSON.parse(<string>route.query?.params) : '',
			});
		} else {
			router.push('/');
		}
		// 登录成功提示
		const signInText = t('message.signInText');
		ElMessage.success(`${currentTimeInfo}，${signInText}`);
		// 添加 loading，防止第一次进入界面时出现短暂空白
		NextLoading.start();
	}
};
const enterHandler = async (event: KeyboardEvent) => {
	if (event.key === 'Enter') {
		await onSignIn();
	}
};
onMounted(() => {
	window.addEventListener('keydown', enterHandler);
});
onBeforeUnmount(() => {
	window.removeEventListener('keydown', enterHandler);
});
</script>

<style scoped lang="scss">
.login-content-form {
	margin-top: 20px;
	@for $i from 1 through 4 {
		.login-animation#{$i} {
			opacity: 0;
			animation-name: error-num;
			animation-duration: 0.5s;
			animation-fill-mode: forwards;
			animation-delay: calc($i/10) + s;
		}
	}
	.login-content-password {
		display: inline-block;
		width: 20px;
		cursor: pointer;
		&:hover {
			color: #909399;
		}
	}
	.login-content-code {
		width: 100%;
		padding: 0;
		font-weight: bold;
		letter-spacing: 5px;
	}
	.login-content-submit {
		width: 100%;
		letter-spacing: 2px;
		font-weight: 300;
		margin-top: 15px;
	}
}
</style>
