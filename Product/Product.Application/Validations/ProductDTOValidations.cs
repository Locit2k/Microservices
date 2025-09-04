using FluentValidation;
using Product.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Validations
{
    public class ProductDTOValidations : AbstractValidator<ProductDTO>
    {
        public ProductDTOValidations()
        {
            RuleFor(x => x.ProductID)
                .NotEmpty().WithMessage("Mã sản phẩm không được bỏ trống.");

            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Tên sản phẩm không được bỏ trống.");

            RuleFor(x => x.Description).NotEmpty()
                .WithMessage("Mô tả sản phẩm không được bỏ trống.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Giá sản phẩm phải lớn hơn hoặc bằng 0.");

            RuleFor(x => x.Stock).GreaterThanOrEqualTo(0)
                .WithMessage("Số lượng sản phẩm trong kho phải lớn hơn hoặc bằng 0.");

            RuleFor(x => x.CategoryID).NotEmpty()
                .WithMessage("Loại sản phẩm không được bỏ trống.");

        }
    }
}
