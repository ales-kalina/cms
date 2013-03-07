using System.Linq;

namespace CMS.ContactManagement.Contract
{

    public interface IContactManager
    {

        Contact GetContact(int contactId);
        IQueryable<Contact> GetContacts();

    }

}