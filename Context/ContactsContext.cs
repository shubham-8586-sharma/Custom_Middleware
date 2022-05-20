using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using models;

namespace Context
{
    public class ContactsContext:DbContext
    {
        public ContactsContext(DbContextOptions<ContactsContext> options)
            :base(options)
         {

          }
     
        public DbSet<Contacts> Contacts { get; set; }
    }

}