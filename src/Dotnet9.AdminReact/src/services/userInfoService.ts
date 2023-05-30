import request from "../utils/request";

export default class UserInfoService {
    public static Login(account:string,password:string){
        return request.post("/api/v1/UserInfoes/Login?account="+account+"&password="+password);
    }
}
