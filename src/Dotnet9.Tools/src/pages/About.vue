<script setup lang="ts">
import { useHead } from '@vueuse/head';
import { useTracker } from '@/modules/tracker/tracker.services';

useHead({ title: 'About - IT Tools' });
const { tracker } = useTracker();
</script>

<template>
  <div mx-auto mt-50px max-w-600px>
    <h1>{{ $t('about.h1') }}</h1>
    <p text-justify>
      {{ $t('about.h1p1') }}
      <c-link href="https://github.com/CorentinTh/it-tools" rel="noopener" target="_blank">
        it-tools
      </c-link>{{ $t('about.h1p2') }}
    </p>
    <p text-justify>
      {{ $t('about.h1p3') }}
      <c-link href="https://github.com/CorentinTh" target="_blank" rel="noopener">
        Corentin Thomasset
      </c-link>{{ $t('about.h1p4') }}
      {{ $t('about.h1p5') }}
    </p>
    <p text-justify>
      {{ $t('about.h1p6') }}
      <c-link
        href="https://dotnet9.com/donation"
        rel="noopener"
        target="_blank"
        @click="() => tracker.trackEvent({ eventName: 'Support button clicked' })"
      >
        {{ $t('about.h1p7') }}
      </c-link>。
    </p>

    <h2>{{ $t('about.h2') }}</h2>
    <p text-justify>
      {{ $t('about.h2p1') }}
      <c-link href="https://github.com/dotnet9/Dotnet9/blob/develop/src/Dotnetools/package.json" rel="noopener" target="_blank">
        package.json
      </c-link>
      {{ $t('about.h2p2') }}
    </p>

    <h2>{{ $t('about.h3') }}</h2>
    <p text-justify>
      {{ $t('about.h3p1') }}
      <c-link
        href="https://github.com/dotnet9/Dotnet9/issues/new/choose"
        rel="noopener"
        target="_blank"
      >
        {{ $t('about.h3p2') }}
      </c-link>
      {{ $t('about.h3p3') }}
    </p>
    <p text-justify>
      {{ $t('about.h3p4') }}
      <c-link
        href="https://github.com/dotnet9/Dotnet9/issues/new/choose"
        rel="noopener"
        target="_blank"
      >
        {{ $t('about.h3p5') }}
      </c-link>
      {{ $t('about.h3p6') }}
    </p>
  </div>
</template>
