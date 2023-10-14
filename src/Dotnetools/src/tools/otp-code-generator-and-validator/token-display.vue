<script setup lang="ts">
import { useCopy } from '@/composable/copy';

const props = defineProps<{ tokens: { previous: string; current: string; next: string } }>();
const { copy: copyPrevious, isJustCopied: previousCopied } = useCopy({ createToast: false });
const { copy: copyCurrent, isJustCopied: currentCopied } = useCopy({ createToast: false });
const { copy: copyNext, isJustCopied: nextCopied } = useCopy({ createToast: false });

const { tokens } = toRefs(props);
</script>

<template>
  <div>
    <div class="labels" w-full flex items-center>
      <div flex-1 text-left>
        上一个
      </div>
      <div flex-1 text-center>
        当前一次性密码
      </div>
      <div flex-1 text-right>
        下一个
      </div>
    </div>
    <n-input-group>
      <n-tooltip trigger="hover" placement="bottom">
        <template #trigger>
          <c-button important:h-12 data-test-id="previous-otp" @click.prevent="copyPrevious(tokens.previous)">
            {{ tokens.previous }}
          </c-button>
        </template>
        <div>{{ previousCopied ? '已复制!' : '复制上一个一次性密码' }}</div>
      </n-tooltip>
      <n-tooltip trigger="hover" placement="bottom">
        <template #trigger>
          <c-button
            data-test-id="current-otp"
            class="current-otp"
            important:h-12
            @click.prevent="copyCurrent(tokens.current)"
          >
            {{ tokens.current }}
          </c-button>
        </template>
        <div>{{ currentCopied ? '已复制!' : '复制当前一次性密码' }}</div>
      </n-tooltip>
      <n-tooltip trigger="hover" placement="bottom">
        <template #trigger>
          <c-button important:h-12 data-test-id="next-otp" @click.prevent="copyNext(tokens.next)">
            {{
              tokens.next
            }}
          </c-button>
        </template>
        <div>{{ nextCopied ? '已复制!' : '复制下一个一次性密码' }}</div>
      </n-tooltip>
    </n-input-group>
  </div>
</template>

<style scoped lang="less">
.current-otp {
  font-size: 22px;
  flex: 1 0 35% !important;
}

.n-button {
  height: 45px;
}

.labels {
  div {
    padding: 0 2px 6px 2px;
    line-height: 1.25;
  }
}

.n-input-group > * {
  flex: 1 0 0;
}
</style>
