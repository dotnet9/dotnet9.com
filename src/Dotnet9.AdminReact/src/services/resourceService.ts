import { CreateResourceDto } from "../models/blogger";
import request from "../utils/request";

class ResourceService {
    public static GetList(keywords: string | null, page: number, pageSize: number): Promise<any> {
        return request.get('/api/v1/Resources/List', {
            keywords,
            page,
            pageSize
        });
    }

    public static Create(dto: CreateResourceDto) {
        return request.post('/api/v1/Resources', dto);
    }

    public static Praise(id: string) {
        return request.post(`/api/v1/Resources/Praise/${id}`);
    }

    public static Download(id: string) {
        return request.post(`/api/v1/Resources/Download/${id}`);
    }
}

export {
    ResourceService
}