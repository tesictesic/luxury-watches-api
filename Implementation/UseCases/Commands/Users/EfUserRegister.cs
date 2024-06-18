using Application.DTO;
using Application.DTO.User;
using Application.Email;
using Application.UseCases.Commands.UserCommands;
using DataAcess;
using Domain;
using FluentValidation;
using Implementation.Validations.User;
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
        private readonly IEmailSender emailSender;
        public EfUserRegister(UserRegisterDTOValidation validations,IEmailSender emailSender,ASPContext context) : base(context)
        {
            this.validations=validations;
            this.emailSender=emailSender;
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
                 profile_path = PictureUpload.Upload(data.Image, "users");

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
            EmailDTO email = new EmailDTO
            {
                Subject = "Registration",
                Content = "<h1>Successfull registration!</h1>",
                SendTo=data.Email
            };
            emailSender.SendEmail(email);
        }
    }
}
