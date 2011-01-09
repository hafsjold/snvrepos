using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Contacts;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.Contacts;

namespace nsPuls3060
{
    class clsGoogle
    {
        static string HRefMedlem = "";
        static RequestSettings rs;
        static ContactsRequest cr;

        public clsGoogle()
        {
            rs = new RequestSettings("puls3060-medlemmerApp-1", "mogens.hafsjold@gmail.com", "MoHa3060");
            rs.AutoPaging = true;
            cr = new ContactsRequest(rs);
        }

        public void test() 
        {
            findGroup("Puls3060Medlemmer");

            var qry = from m in Program.karMedlemmer 
                      where m.erMedlem() == true && m.Email.Length > 0
                      select m;
            if (qry.Count() > 0)
            {
                foreach (var medlem in qry)
                {
                    addMedlem(medlem.Navn, medlem.Email);
                
                }
            }
                    }

        private static void addMedlem(string p_navn, string p_email)
        {
            Contact newContact = new Contact();
            newContact.Title = p_navn;

            EMail primaryEmail = new EMail(p_email);
            primaryEmail.Primary = true;
            primaryEmail.Rel = ContactsRelationships.IsHome;
            newContact.Emails.Add(primaryEmail);

            GroupMembership gm = new GroupMembership();
            gm.HRef = HRefMedlem;
            newContact.GroupMembership.Add(gm);

            

            Uri feedUri = new Uri(ContactsQuery.CreateContactsUri("default"));
            Contact createdContact = cr.Insert(feedUri, newContact);
        }

        private static void findGroup(string groupname)
        {
            GroupsQuery query = new GroupsQuery(GroupsQuery.CreateGroupsUri("default"));
            query.StartDate = new DateTime(2008, 1, 1);
            Feed<Group> feed = cr.Get<Group>(query);

            foreach (Group group in feed.Entries)
            {
                if (group.Title == groupname)
                {
                    HRefMedlem = group.Id;
                }
            }
        }
    }
}
