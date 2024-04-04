using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlumHub
{
    enum ROLES
    {
        [Description("Client")]
        CLIENT,
        [Description("Master")]
        MASTER,
        [Description("Admin")]
        ADMIN
    }
}
