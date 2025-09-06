using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth.Application.DTOs;

namespace Auth.Application.Validations
{
    public class RegisterDTOValidations : AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidations()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Họ và tên không được bỏ trống.");

            RuleFor(x => x.Gender)
                .Must(g => string.IsNullOrEmpty(g) || g == "Nam" || g == "Nữ")
                .WithMessage("Giới tính không hợp lệ.");

            RuleFor(x => x.Birthday)
                .NotNull().WithMessage("Ngày sinh không được bỏ trống.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Ngày sinh không được lớn hơn ngày hiện tại.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Số điện thoại không được bỏ trống.")
                .Matches(@"^\d{10,15}$").WithMessage("Số điện thoại không hợp lệ.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email không được bỏ trống.")
                .EmailAddress().WithMessage("Email không hợp lệ.");

            RuleFor(x => x.UserID)
                .NotEmpty().WithMessage("Tên tài khoản không được bỏ trống")
                .MinimumLength(6).WithMessage("Tên tài khoản không hợp lệ.")
                .Matches("^[a-zA-Z0-9]+$").WithMessage("Tên tài khoản không chứa ký tự đặc biệt."); ;
        }
    }
}
