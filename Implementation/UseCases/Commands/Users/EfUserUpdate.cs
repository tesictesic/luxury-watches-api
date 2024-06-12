using Application.DTO.User;
using Application.UseCases.Commands.UserCommands;
using DataAcess;
using Domain;
using FluentValidation;
using FluentValidation.Internal;
using Implementation.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Users
{
    public class EfUserUpdate : EfUseCase,IUserUpdateCommand
    {
        private UpdateUserDTOValidation _validation;
        public EfUserUpdate(UpdateUserDTOValidation validation,ASPContext context) : base(context)
        {
            this._validation = validation;
        }

        public int Id => 12;

        public string Name => this.GetType().Name;

        public void Execute(RegisterDTO data)
        {
            User user_obj=Context.Users.Find(data.Id);
            if (user_obj == null)
            {
                throw new InvalidOperationException("User is not found");
            }
            _validation.ValidateAndThrow(data);
            
            string file_path = "";
            if (data.Image.Length > 0)
            {
                file_path=PictureUpload.Upload(data.Image,"users");
            };
            user_obj.First_Name = data.First_Name;
            user_obj.Last_Name=data.Last_Name;
            user_obj.Email = data.Email;
            user_obj.Password = BCrypt.Net.BCrypt.HashPassword(data.Password);
            user_obj.Picture = file_path;

            Context.SaveChanges();
            


        }
    }
}
