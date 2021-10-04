
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain
{
    public class ApplicationUser :IdentityUser<Guid>
    {
        public List<Task> Tasks { get; set; }
        public List<Team> Teams { get; set; }
    }

    public class ApplicationUserLogin : IdentityUserLogin<Guid> { }
    public class ApplicationUserRole : IdentityUserRole<Guid> { }
    public class ApplicationUserClaim : IdentityUserClaim<Guid> { }
    public class ApplicationRole : IdentityRole<Guid> { }
    public class ApplicationUserToken : IdentityUserToken<Guid> { }

    public class ApplicationClaimsPrincipal : ClaimsPrincipal
    {
        public ApplicationClaimsPrincipal(ClaimsPrincipal principal) : base(principal)
        { }

        public Guid UserId
        {
            get { return Guid.Parse(this.FindFirst(ClaimTypes.Sid).Value); }
        }
    }

}
