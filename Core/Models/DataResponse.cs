using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class DataResponse<T>
    {
        /// <summary>
        /// Mã trạng thái HTTP của phản hồi.
        /// </summary>
        public int StatusCode { get; set; } = 200;

        /// <summary>
        /// Thông báo chung về kết quả của yêu cầu.
        /// Có thể là thông báo thành công, lỗi hoặc cảnh báo.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Dữ liệu thực tế mà API trả về khi yêu cầu thành công.
        /// Kiểu dữ liệu sẽ phụ thuộc vào từng API cụ thể (ví dụ: User, List<Product>).
        /// Sẽ là null nếu có lỗi hoặc không có dữ liệu cần trả về.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Thông tin chi tiết về lỗi nếu yêu cầu không thành công.
        /// Sẽ là null nếu yêu cầu thành công.
        /// </summary>
        public ErrorDetails Error { get; set; }

        // Constructor cho trường hợp thành công
        public DataResponse(int statusCode, string message, T data)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
            Error = null; // Không có lỗi khi thành công
        }

        // Constructor cho trường hợp lỗi
        public DataResponse(int statusCode, string message, ErrorDetails error = null)
        {
            StatusCode = statusCode;
            Message = message;
            Data = default; // Dữ liệu là giá trị mặc định khi có lỗi
            Error = error;
        }

        // Constructor mặc định (có thể không cần dùng nhiều nếu luôn dùng các constructor trên)
        public DataResponse() { }
    }

    /// <summary>
    /// Class để chứa thông tin chi tiết về lỗi.
    /// </summary>
    public class ErrorDetails
    {
        /// <summary>
        /// Mã lỗi cụ thể (ví dụ: "VALIDATION_ERROR", "UNAUTHORIZED").
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Mô tả chi tiết về lỗi.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Danh sách các lỗi chi tiết hơn, đặc biệt hữu ích cho lỗi validation.
        /// Mỗi dictionary có thể chứa "field" và "message".
        /// </summary>
        public List<Dictionary<string, string>> Details { get; set; }
    }

    public static class ErrorCodes
    {
        public const string VALIDATION_ERROR = "VALIDATION_ERROR";
        public const string UNAUTHORIZED = "UNAUTHORIZED";
        public const string FORBIDDEN = "FORBIDDEN";
        public const string NOT_FOUND = "NOT_FOUND";
        public const string INTERNAL_SERVER_ERROR = "INTERNAL_SERVER_ERROR";
        public const string BAD_REQUEST = "BAD_REQUEST";
    }
}
