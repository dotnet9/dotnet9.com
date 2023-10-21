import { BaseApi } from './BaseApi';
import type { AddFriendLinkInput, PageResultFriendLinkPageOutput, UpdateFriendLinkInput } from './models';
/**
 * 友情链接
 */
class FriendLink extends BaseApi<AddFriendLinkInput, UpdateFriendLinkInput, PageResultFriendLinkPageOutput> {
	constructor() {
		super('/friendlink/');
	}
}

export default new FriendLink();
