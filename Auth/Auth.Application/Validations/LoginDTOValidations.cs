using Auth.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Validations
{
    public class LoginDTOValidations : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidations()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Tên tài khoản không được bỏ trống.")
                .MinimumLength(6).WithMessage("Tên tài khoản không hợp lệ.")
                .Matches("^[a-zA-Z0-9]+$").WithMessage("Tên tài khoản không chứa ký tự đặc biệt.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Mật khẩu không được bỏ trống.")
                .MinimumLength(6).WithMessage("Mật khẩu không hợp lệ.")
                .Matches("^[a-zA-Z0-9]+$").WithMessage("Mật khẩu không chứa ký tự đặc biệt.");
        }
    }
}
