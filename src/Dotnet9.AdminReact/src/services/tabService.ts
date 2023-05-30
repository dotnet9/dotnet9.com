import request from "../utils/request";

class TabService {
    public static getTabs(): Promise<any> {
        return request.get("/api/v1/Tabs/List");
    }

    public static create(name:string){
        return request.post("/api/v1/Tabs?name="+name);
    }
}

export{
    TabService
}