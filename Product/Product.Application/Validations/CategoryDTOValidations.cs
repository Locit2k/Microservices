using FluentValidation;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Validations
{
    public class CategoryDTOValidations : AbstractValidator<Categories>
    {
        public CategoryDTOValidations()
        {
            RuleFor(x => x.CategoryID).NotEmpty().WithMessage("Mã danh mục không được bỏ trống.");

            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Tên danh mục không được bỏ trống.");
        }
    }
}
