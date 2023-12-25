using Microsoft.AspNet.Identity;
using System;
using System.Security.Claims;
using System.Security.Principal;

namespace HTTTQLDanSo.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetRegionID(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            if (ci != null)
            {
                return ci.FindFirstValue("RegionID");
            }
            return null;
        }

        public static string GetFullName(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            if (ci != null)
            {
                return $"{ci.FindFirstValue("LastName")} {ci.FindFirstValue("FirstName")}";
            }
            return null;
        }

        public static int? GetWorkerId(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            if (ci != null && int.TryParse(ci.FindFirstValue("WorkerId"), out int workerId))
            {
                return workerId;
            }

            return null;
        }
    }
}