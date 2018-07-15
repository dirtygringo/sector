namespace NM.SharedKernel.Common.Claims
{
    public static class CustomClaimTypes
    {
        public const string Id = "http://nm.schemas.xmlsoap.org/ws/2018/02/identity/claims/id";
        public const string Email = "http://nm.schemas.xmlsoap.org/ws/2018/02/identity/claims/email";
        public const string FirstName = "http://nm.schemas.xmlsoap.org/ws/2018/02/identity/claims/firstname";
        public const string LastName = "http://nm.schemas.xmlsoap.org/ws/2018/02/identity/claims/lastname";

        public const string ApiAccess = "http://nm.schemas.xmlsoap.org/ws/2018/02/identity/claims/api_access";
        public const string AdminAccess = "http://nm.schemas.xmlsoap.org/ws/2018/02/identity/claims/admin";
    }
}
