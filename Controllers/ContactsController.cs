using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using models;
using Repositories;

namespace Controllers
{
     [ApiController]
  [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        public IContactsRepository ContactsRepo { get; set; }

        public ContactsController(IContactsRepository _repo)
        {
            ContactsRepo = _repo;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contactList =  await ContactsRepo.GetAll();
            return Ok(contactList);
        }

        [HttpGet("{id}", Name = "GetContacts")]
        public async Task<IActionResult> GetById(string id)
        {
            var item = await ContactsRepo.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Contacts item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            await ContactsRepo.Add(item);
            return CreatedAtRoute("GetContacts", new { Controller = "Contacts", id = item.MobilePhone }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Contacts item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var contactObj = await ContactsRepo.Find(id);
            if (contactObj == null)
            {                
                return NotFound();
            }
            await ContactsRepo.Update(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await ContactsRepo.Remove(id);
            return NoContent();
        }
    }
}