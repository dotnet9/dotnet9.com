
declare let DotNet: any;

let assemblyName = 'WebEditor';

class DotentUtil {

    /**
     * 初始化
     * @param url 
     */
    public static async Init(url: string) {
        await DotNet.invokeMethodAsync(assemblyName, 'Init', url);
    }

    /**
     * 加载程序集
     * @param url 
     */
    public static async LoadAssembly(url: string[]) {
        await DotNet.invokeMethodAsync(assemblyName, 'LoadAssembly', url);
    }

    /**
     * 删除加载的程序集
     * @param code 
     * @returns 
     */
    public static async RemoveAssembly(assembly: string): Promise<boolean> {
        return await DotNet.invokeMethodAsync(assemblyName, 'RemoveAssembly', assembly);
    }

    /**
     * 删除加载的程序集
     * @param code 
     * @returns 
     */
    public static async GetLoadAssemblys(): Promise<string[]> {
        return await DotNet.invokeMethodAsync(assemblyName, 'GetLoadAssemblys');
    }

    /**
     * 执行代码
     * @param code 
     * @returns 
     */
    public static async Execute(code: string) {
        return await DotNet.invokeMethodAsync(assemblyName, 'Execute', code);
    }
}

export {
    DotentUtil
}