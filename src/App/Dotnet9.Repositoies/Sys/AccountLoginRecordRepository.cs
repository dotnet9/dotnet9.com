using Dotnet9.Models.Dtos.Account;
using Dotnet9Tools.Helper;

namespace Dotnet9.Repositoies.Sys;

/// <summary>
///     登录日志
/// </summary>
public class AccountLoginRecordRepository : BaseRepository<AccountLoginRecord, int>
{
    public AccountLoginRecordRepository(DbContext ctx) : base(ctx)
    {
    }

    /// <summary>
    ///     写入登录记录
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="ip"></param>
    /// <param name="ua"></param>
    /// <param name="message"></param>
    /// <param name="loginSuccess"></param>
    public async Task AddRecordAsync(Guid? accountId, string ip, string ua, string message, bool loginSuccess)
    {
        Accounts? account = await Ctx.Set<Accounts>().FindAsync(accountId);
        if (account == null)
        {
            return;
        }

        await Ctx.Set<AccountLoginRecord>().AddAsync(new AccountLoginRecord
        {
            Account = account!,
            Ip = ip,
            IsLogin = loginSuccess,
            UA = ua,
            Message = message,
            CreateTime = DateTime.Now
        });
        await Ctx.SaveChangesAsync();
    }

    public async Task<PageDto<AccountLoginRecordDto>> GetLoginRecord(BasePageModel model, Guid? accountId)
    {
        int count = await Ctx.Set<AccountLoginRecord>().CountAsync();
        List<AccountLoginRecordDto> res = await Ctx.Set<AccountLoginRecord>()
            .WhereIf(accountId != null, a => a.Account.Id == accountId)
            .Include(a => a.Account)
            .OrderByDescending(a => a.CreateTime)
            .Page(model.Index, model.PageSize).Select(
                a => new AccountLoginRecordDto
                {
                    AccountName = a.Account.UserName,
                    CreateTime = a.CreateTime,
                    Ip = a.Ip,
                    UserAgent = a.UA
                }).ToListAsync();
        return new PageDto<AccountLoginRecordDto>(count, res);
    }
}