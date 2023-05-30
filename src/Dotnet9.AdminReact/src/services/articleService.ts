import { CreateArticleDto } from "../models/blogger";
import request from "../utils/request";

class ArticleService {
    public static getList(keywords: string, categoryId: string | null, tabIds: string | null, page: number = 1, pageSize: number = 20): Promise<any> {

        if (categoryId) {
            return request.get("/api/v1/Articles/List", {
                keywords,
                categoryId,
                page,
                pageSize,
                tabIds
            });
        } else {
            return request.get("/api/blogs/search", {
                keywords,
                page,
                pageSize,
                tabIds
            });
        }
    }

    /**
     * 得到详细
     * @param id 
     * @returns 
     */
    public static get(id: string): Promise<any> {
        return request.get(`/api/v1/Articles/${id}`);
    }

    public static getRanking() {
        return request.get("/api/v1/Articles/Ranking");
    }

    public static Create(dto:CreateArticleDto): Promise<any> {
        return request.post("/api/v1/Articles", dto);
    }

    public static delete(id:string){
        return request.delete(`/api/v1/Articles/${id}`);
    }

    public static like(id:string){
        return request.post(`/api/v1/Articles/Like/${id}`);
    }
}

export {
    ArticleService
}