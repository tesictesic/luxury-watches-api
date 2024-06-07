using Application.DTO;
using Application.UseCases.Commands.UserCommands;
using DataAcess;
using Domain;
using FluentValidation;
using Implementation.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Users
{
    public class EfUserRegister : EfUseCase,IUserRegisterCommand
    {
        private readonly UserRegisterDTOValidation validations;
        public EfUserRegister(UserRegisterDTOValidation validations,ASPContext context) : base(context)
        {
            this.validations=validations;
        }

        public int Id => 6;

        public string Name => this.GetType().Name;

        public void Execute(RegisterDTO data)
        {
            List<string> allowedExtensions = new List<string> { ".png",".jpg",".jpeg"};
            string profile_path = "";
            validations.ValidateAndThrow(data);
            if(data.Image != null)
            {
                var extension = Path.GetExtension(data.Image.FileName);
                if (!allowedExtensions.Contains(extension.ToLower()))
                {
                    throw new ArgumentException();
                }
                var fileName = Guid.NewGuid().ToString() + extension;

                var savePath = Path.Combine("wwwroot", "users", fileName);
                using var fs = new FileStream(savePath, FileMode.Create);
                data.Image.CopyTo(fs);
                profile_path = "/users/" + fileName;
            }
            User user = new User
            {
                First_Name = data.First_Name,
                Last_Name = data.Last_Name,
                Email = data.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(data.Password),
                Picture = profile_path
      
                
            };
            Context.Add(user);
            Context.SaveChanges();
        }
    }
}
