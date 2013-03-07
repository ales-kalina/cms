using System;
using System.Collections.Generic;
using System.Linq;

using CMS.ContactManagement.Contract;

namespace CMS.ContactManagement
{

    public class ContactManager : IContactManager
    {

        private IList<Contact> mContacts;

        public ContactManager()
        {
            mContacts = CreateContacts();
        }

        public Contact GetContact(int contactId)
        {
            return mContacts.SingleOrDefault(x => x.Id == contactId);
        }

        public IQueryable<Contact> GetContacts()
        {
            return mContacts.AsQueryable();
        }

        private IList<Contact> CreateContacts()
        {
            List<Contact> contacts = new List<Contact>(10);
            for (int i = 1; i <= 10; i++)
            {
                Contact contact = new Contact
                {
                    Id = i,
                    Name = String.Format("Contact {0}", i)
                };
                contacts.Add(contact);
            }

            return contacts;
        }

    }

}