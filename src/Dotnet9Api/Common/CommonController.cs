using System.ComponentModel.DataAnnotations;
using Dotnet9.Models;
using Dotnet9.Models.Data.Entitys;
using Lazy.Captcha.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using CaptchaHelper = Dotnet9Tools.Helper.CaptchaHelper;

namespace Dotnet9Api.Common;

/// <summary>
///     公共操作
/// </summary>
public class CommonController : BaseAdminController
{
    private readonly DbContext _dbcontext;

    public CommonController(DbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    /// <summary>
    ///     上传
    /// </summary>
    /// <param name="host"></param>
    /// <param name="isTag"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Upload([FromServices] IWebHostEnvironment host, [FromQuery] bool isTag = true)
    {
        IFormFileCollection files = Request.Form.Files;
        long size = files.Sum(f => f.Length);
        List<string> list = new List<string>();
        foreach (IFormFile formFile in files)
        {
            if (formFile.Length > 0)
            {
                string path = Path.Combine(host.WebRootPath, "files", DateTime.Now.ToString("yyyyMMdd"));

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fileName = $"{Guid.NewGuid():N}{Path.GetExtension(formFile.FileName)}";
                path = Path.Combine(path, fileName);
                string filePath = path;
                using MemoryStream ms = new MemoryStream();
                await using FileStream stream = System.IO.File.Create(filePath);
                if (isTag)
                {
                    ImageHelper.WriteWaterTag(ms.ToArray(), "dotnet9.com", 10);
                }
                else
                {
                    await formFile.CopyToAsync(ms);
                }


                string file =
                    $"/{Path.Combine("files", fileName).Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)}";
                list.Add(file);
                await _dbcontext.Set<SysResource>().AddAsync(new SysResource
                {
                    Path = file,
                    Size = new FileInfo(filePath).Length,
                    Suffix = Path.GetExtension(formFile.FileName),
                    ClientIp = HttpContext.GetClientIp(),
                    StorageType = StorageType.Local
                });
            }
        }

        await _dbcontext.SaveChangesAsync();

        return Ok(new { list, size });
    }

    /// <summary>
    ///     获取验证码
    /// </summary>
    /// <param name="captcha"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public IActionResult GetCaptcha([FromServices] ICaptcha captcha, [Required] ValidateCodeType type)
    {
        (string id, string guid) = CaptchaHelper.GenerateId((int)type);
        CaptchaData? info = captcha.Generate(id, 60 * 3);
        // 有多处验证码且过期时间不一样，可传第二个参数覆盖默认配置。
        //var info = _captcha.Generate(id,120);
        MemoryStream stream = new MemoryStream(info.Bytes);
        HttpContext.Response.Cookies.Append("captcha-id", guid);
        return File(stream, "image/gif");
    }
}