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
            return Ok(dbContext.Contacts.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> AddContacts( AddContactsRequest addContacts)
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

        [HttpPut]
        [Route("{id: guid}")]

        public  async Task<IActionResult> UpdateContacts([FromRoute] Guid id, UpdateControllerRequest updaterequest)
        {
            var con = dbContext.Contacts.Find(id);
            if (con != null)
            {
                con.FullName = updaterequest.FullName;
                con.Email = updaterequest.Email;
                con.Phone = updaterequest.Phone;
                con.Address = updaterequest.Address;

                 dbContext.SaveChanges();
                return Ok(con);
            }
            return NotFound();

        }
    }
}
