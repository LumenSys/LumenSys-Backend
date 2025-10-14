using System.Security.Claims;

public class CompanyProvider : ICompanyProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CompanyProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int GetCompanyId()
    {
        var claimValue = _httpContextAccessor.HttpContext?.User?.FindFirst("CompanyId")?.Value;
        return int.TryParse(claimValue, out var id) ? id : 0;
    }

    public string GetUserRole()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value ?? "";
    }

}
