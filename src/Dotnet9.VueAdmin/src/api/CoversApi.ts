import { BaseApi } from './BaseApi';
import type { AddCoversInput, CoversPageOutput, UpdateCoversInput } from './models';
// 模块封面相关Api
class CoversApi extends BaseApi<AddCoversInput, UpdateCoversInput, CoversPageOutput> {
	constructor() {
		super('/covers/');
	}
}
export default new CoversApi();
