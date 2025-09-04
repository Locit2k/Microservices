using FluentValidation;
using Order.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Validations
{
    public class OrderDTOValidations : AbstractValidator<OrderDTO>
    {
        public OrderDTOValidations()
        {
            RuleFor(x => x.OrderDate).NotEmpty().WithMessage("Ngày mua hàng không được bỏ trống.");

            RuleFor(x => x.UserID).NotEmpty().WithMessage("Thông tin người dùng không được bỏ trống.");

            RuleFor(x => x.PaymentID).NotEmpty().WithMessage("Thông tin thanh toán không được bỏ trống");

            RuleFor(x => x.ShippingAddress).NotEmpty().WithMessage("Địa chỉ giao hàng không được bỏ trống");

            RuleFor(x => x.Details)
                .NotEmpty().WithMessage("Chi tiết đơn hàng không được bỏ trống.")
                .Must(y => y.Count() > 0).WithMessage("Đơn hàng phải có ít nhất một chi tiết.")
                .ForEach(z =>
                {
                    z.ChildRules(child =>
                    {
                        child.RuleFor(x => x.ProductID).NotEmpty().WithMessage("Mã sản phẩm không được bỏ trống.");
                        child.RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Số lượng sản phẩm phải lớn hơn 0.");
                    });
                });
        }
    }
}
