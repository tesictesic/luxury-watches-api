using Application.DTO.Contacts;
using Application.UseCases.Commands.ContactCommands;
using DataAcess;
using Domain;
using FluentValidation;
using Implementation.Validations.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Contacts
{
    public class EfCreateContact : EfUseCase,ICreateContact
    {
        private CreateContactValidator _validator;

        public EfCreateContact(CreateContactValidator validator, ASPContext context) : base(context)
        {
            this._validator = validator;
        }

        public int Id => 32;

        public string Name => this.GetType().Name;

        public void Execute(InsertContactDTO data)
        {
            _validator.ValidateAndThrow(data);
            Contact contact = new Contact
            {
                Name = data.Name,
                Subject = data.Subject,
                Email = data.Email,
                Body = data.Body,
            };
            Context.Contacts.Add(contact);
            Context.SaveChanges();
        }
    }
}
