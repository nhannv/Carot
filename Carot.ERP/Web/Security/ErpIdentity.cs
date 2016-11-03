using System;
using System.Security.Claims;
using Carot.ERP.Api.Models;

namespace Carot.ERP.Web.Security
{
    public class ErpIdentity : ClaimsIdentity
    {
            public override string AuthenticationType { get { return "CarotErp"; } }

            public override bool IsAuthenticated { get { return User != null && !String.IsNullOrWhiteSpace(User.Email); } }

            public ErpUser User { get; set; }
    }
}
