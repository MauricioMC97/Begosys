using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace BegoSys.Web.Models
{
    public class CstmPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }

        public CstmPrincipal(CstmIdentity customIdentity)
        {
            this.Identity = customIdentity;
        }

        public bool IsInRole(string role)
        {
            //i am not sure if i can use this func
            return Roles.IsUserInRole(role);
        }
    }

    public class CstmIdentity : IIdentity
    {
        public string Name { get; private set; }

        public CstmIdentity(string name)
        {
            this.Name = name;
        }

        public string AuthenticationType
        {
            get { return "custom"; }
        }

        public bool IsAuthenticated
        {
            get { return !string.IsNullOrEmpty(this.Name); }
        }
    }
}