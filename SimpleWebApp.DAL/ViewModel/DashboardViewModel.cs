using SimpleWebApp.data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebApp.data.ViewModel
{
    public class DashboardViewModel
    {
        public List<UserDetails> ClientList { get; set; } = new List<UserDetails>();
        public List<UserDetails> AdminList { get; set; } = new List<UserDetails>();
    }
}
