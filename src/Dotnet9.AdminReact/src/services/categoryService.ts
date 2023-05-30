import request from "../utils/request";

class CategoryService {
    public static getList(): Promise<any> {
        return request.get("/api/v1/Categories/List");
    }

    public static create(name:string,description:string){
        return request.post("/api/v1/Categories?name="+name+"&description="+description);
    }

}

export {
    CategoryService 
}