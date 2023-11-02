import { expect, test } from '@playwright/test';

test.describe('工具 - Mac地址生成器', () => {
  test.beforeEach(async ({ page }) => {
    await page.goto('/mac-address-generator');
  });
  test('Has correct title', async ({ page }) => {
    await expect(page).toHaveTitle('Mac地址生成器 - Dotnet工具箱');
  });
});
