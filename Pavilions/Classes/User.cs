using System;
using System.Collections.Generic;

#nullable disable

namespace Pavilions
{
    public partial class User
    {
        public int IdStaffer { get; set; }
        public string LoginUser { get; set; }
        public string PasswordUser { get; set; }
        public int? IdRole { get; set; }

        public virtual Role IdRoleNavigation { get; set; }
        public virtual Staffer IdStafferNavigation { get; set; }
    }
}
