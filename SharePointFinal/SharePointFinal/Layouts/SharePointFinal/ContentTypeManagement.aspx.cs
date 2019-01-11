using System;
using System.IO;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace SharePointFinal.Layouts.SharePointFinal
{
    public partial class ContentTypeManagement : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (SPSite site = new SPSite(@"http://etgs-uni-cam:8081/"))
            {
                using (SPWeb web = site.OpenWeb())
                {                   
                    string contentTypeName = "LUStudents";
                    string fullNameFieldName = "Student Full Name";
                    string depNameFieldName = "Departement Name";
                    string regDateFieldName = "Registration Date";
                    string noteFieldName = "Note";
                    SPContentType contentType = web.ContentTypes[contentTypeName];
                    web.AllowUnsafeUpdates = true;
                    if (contentType == null)
                    {                       
                        contentType = new SPContentType(web.ContentTypes["Document"], web.ContentTypes, contentTypeName);
                        contentType.Group = "LUniversity";                        
                        string fullNameFieldLink = web.Fields.Add(fullNameFieldName, SPFieldType.Text, true);
                        string depNameFieldLink = web.Fields.Add(depNameFieldName, SPFieldType.Text, true);
                        string regDateFieldLink = web.Fields.Add(regDateFieldName,SPFieldType.DateTime, true);
                        string noteFieldLink = web.Fields.Add(noteFieldName, SPFieldType.Number, true);                       
                        SPField fullNameField = web.Fields.GetField(fullNameFieldLink);
                        SPField depNameField = web.Fields.GetField(depNameFieldLink);
                        SPField regDateField = web.Fields.GetField(regDateFieldLink);
                        SPField noteField = web.Fields.GetField(noteFieldLink);
                        contentType.FieldLinks.Add(new SPFieldLink(fullNameField));
                        contentType.FieldLinks.Add(new SPFieldLink(depNameField));
                        contentType.FieldLinks.Add(new SPFieldLink(regDateField));
                        contentType.FieldLinks.Add(new SPFieldLink(noteField));
                        web.ContentTypes.Add(contentType);
                        contentType.Update();
                        web.Update();
                    }

                    string listName = "Students";
                    SPList list = web.Lists.TryGetList(listName);
                    if (list == null)
                    {                        
                        string listDescription = "This is a document library that I've created from C# code.";
                        web.Lists.Add(listName, listDescription, SPListTemplateType.DocumentLibrary);
                        list = web.Lists[listName];
                        list.ContentTypes.Add(contentType);
                        list.Update();
                        SPView view = list.Views["All Documents"];
                        view.ViewFields.DeleteAll();
                        view.ViewFields.Add(fullNameFieldName);
                        view.ViewFields.Add(depNameFieldName);
                        view.ViewFields.Add(regDateFieldName);
                        view.ViewFields.Add(noteFieldName);
                        view.Update();
                        web.Update();
                    }            
                }
            }
        }
    }
}
