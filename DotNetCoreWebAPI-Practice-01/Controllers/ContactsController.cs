using DotNetCoreWebAPI_Practice_01.Data;
using DotNetCoreWebAPI_Practice_01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DotNetCoreWebAPI_Practice_01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactAPIDbContext dbContext;

        public ContactsController(ContactAPIDbContext DbContext)
        {
            dbContext = DbContext;
        }
        [HttpGet]
        public IActionResult GetContacts()
        {
            try
            {
                return Ok(dbContext.Contacts.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred In Contact Data Getting : " + ex.Message);
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContactById([FromRoute] Guid id)
        {
            try
            {
                var contact = dbContext.Contacts.Find(id);
                if (contact == null)
                {
                    return NotFound();
                }

                return Ok(contact);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "An error occurred In  Get Contact By Id :" + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddContacts( AddContactsRequest addContacts)
        {

            try
            {
                var contacts = new Contact
                {
                    Id = Guid.NewGuid(),
                    Email = addContacts.Email,
                    FullName = addContacts.FullName,
                    Phone = addContacts.Phone,
                    Address = addContacts.Address
                };
                await dbContext.Contacts.AddAsync(contacts);
                await dbContext.SaveChangesAsync();

                return Ok(contacts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred In Contact Data Posting : " + ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateContacts([FromRoute] Guid id, UpdateControllerRequest updaterequest)
        {
            try
            {
                var contactId = await dbContext.Contacts.FindAsync(id);
                if (contactId != null)
                {
                    contactId.FullName = updaterequest.FullName;
                    contactId.Email = updaterequest.Email;
                    contactId.Phone = updaterequest.Phone;
                    contactId.Address = updaterequest.Address;

                    dbContext.SaveChanges();
                    return Ok(contactId);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred In Contact Data Updating :" + ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteId([FromRoute] Guid id)
        {
            try
            {
                var contact = await dbContext.Contacts.FindAsync(id);

                if (contact != null)
                {
                    dbContext.Contacts.Remove(contact);
                    dbContext.SaveChanges();
                    return Ok(contact);
                }
                return NotFound();
            }
            catch(Exception ex)
            {
                return StatusCode(500, " An error occurred In Delete Contact: " + ex.Message);
            }
            
        }
        

    }
}
