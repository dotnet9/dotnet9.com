import { BaseApi } from './BaseApi';
import { AddCustomConfigItemInput, UpdateCustomConfigItemInput } from './models';

class CustomConfigItemApi extends BaseApi<AddCustomConfigItemInput, UpdateCustomConfigItemInput> {
	constructor() {
		super('customconfigitem');
	}
}

export default new CustomConfigItemApi();
