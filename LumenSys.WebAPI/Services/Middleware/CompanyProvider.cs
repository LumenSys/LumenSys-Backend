using System.Security.Claims;

public class CompanyProvider : ICompanyProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CompanyProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetCompanyId()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst("CompanyId")?.Value ?? "";
    }

    public string GetUserRole()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value ?? "";
    }

}
