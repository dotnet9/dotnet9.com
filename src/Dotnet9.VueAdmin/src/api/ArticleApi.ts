import { BaseApi } from './BaseApi';
import { AddArticleInput, ArticleDetailOutput, ArticlePageOutput, UpdateArticleInput } from './models';

class ArticleApi extends BaseApi<AddArticleInput, UpdateArticleInput, ArticlePageOutput> {
	constructor() {
		super('/article/');
	}
	/**
	 * 文章详情
	 * @param id 文章id
	 * @returns
	 */
	detail = (id: number) => {
		return this.get<ArticleDetailOutput>(`${this.basePath}detail`, { params: { id } });
	};
}
export default new ArticleApi();
