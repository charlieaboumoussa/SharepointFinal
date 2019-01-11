using System;
using System.Collections.Generic;
using System.Web.Services;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.WebControls;
using SharePointFinal.Model;

namespace SharePointFinal.Layouts.SharePointFinal
{
    public partial class PermissionInvitation : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        [WebMethod]
        public static string getPermissions() {
            string retVal = string.Empty;
            List<PermissionInfo> permissionList = new List<PermissionInfo>();
            using (SPSite site = new SPSite(@"http://etgs-uni-cam:8081/"))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    PermissionInfo permission = new PermissionInfo();
                    string listName = "Students";
                    SPList list = web.Lists.TryGetList(listName);
                    if (list == null) {
                        return "No list found!";
                    }                    
                    foreach (SPRoleAssignment roleAssignment in list.RoleAssignments)
                    {
                        if (roleAssignment.Member is SPGroup)
                        {
                            permission.Name = roleAssignment.Member.Name;
                            permission.Type = "";                          
                            foreach (SPRoleDefinition roleDefinition in roleAssignment.RoleDefinitionBindings)
                            {
                                permission.PermissionLevel = roleDefinition.Name;
                            }
                            permissionList.Add(permission);
                        }
                        else if (roleAssignment.Member is SPUser)
                        {
                            permission.Name = roleAssignment.Member.Name;
                            permission.Type = "";
                            foreach (SPRoleDefinition roleDefinition in roleAssignment.RoleDefinitionBindings)
                            {
                                permission.PermissionLevel = roleDefinition.Name;
                            }
                            permissionList.Add(permission);
                        }
                    }
                    
                }
            }
            retVal = Newtonsoft.Json.JsonConvert.SerializeObject(permissionList);
            return retVal;
        }
        [WebMethod]
        public static void breakSecurity()
        {
            using (SPSite site = new SPSite(@"http://etgs-uni-cam:8081/"))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    web.AllowUnsafeUpdates=true;
                    string listName = "Students";
                    SPList list = web.Lists.TryGetList(listName);                   
                    list.BreakRoleInheritance(true);                    
                }
            }
        }
    }
}
