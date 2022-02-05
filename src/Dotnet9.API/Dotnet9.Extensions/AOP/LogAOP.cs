using Castle.DynamicProxy;

namespace Dotnet9.Extensions.AOP;

public class LogAOP : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        var dataIntercept = $"{DateTime.Now.ToString("yyyyMMddHHmmss")} " +
                            $"当前执行方法：{ invocation.Method.Name} " +
                            $"参数是： {string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray())} \r\n";
        
        try
        {
            invocation.Proceed();
        }
        catch (Exception ex)
        {
            dataIntercept += ($"方法执行中出现异常：{ex.Message}");
        }


        dataIntercept += ($"被拦截方法执行完毕，返回结果：{invocation.ReturnValue}");

        #region 输出到当前项目日志
        var path = Directory.GetCurrentDirectory() + @"\Log";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var fileName = path + $@"\InterceptLog-{DateTime.Now.ToString("yyyyMMddHHmmss")}.log";

        StreamWriter sw = File.AppendText(fileName);
        sw.WriteLine(dataIntercept);
        sw.Close();
        #endregion

    }
}