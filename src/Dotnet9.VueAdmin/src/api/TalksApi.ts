import { BaseApi } from './BaseApi';
import type { AddTalksInput, PageResultTalksPageOutput, UpdateTalksInput } from './models';

class TalksApi extends BaseApi<AddTalksInput, UpdateTalksInput, PageResultTalksPageOutput> {
	constructor() {
		super('/talks/');
	}
}
export default new TalksApi();
