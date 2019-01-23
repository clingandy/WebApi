/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2019/1/19 10:50:54
** desc：    ResponseExtension类
** Ver.:     V1.0.0
*********************************************************************************/
using System.ComponentModel;

namespace Infrastructure.Common
{
    public static class ResponseExtension
    {
        public static ApiResult<T> ResponseSuccess<T>(this T obj, string message = "", int total = 0)
        {
            return new ApiResult<T>
            {
                Code = ApiCodeEnum.Success.GetEnumInt(),
                Msg = !string.IsNullOrWhiteSpace(message) ? message : ApiCodeEnum.Success.GetEnumDes(),
                IsOk = true,
                Data = obj,
                Total = total
            };
        }

        public static ApiResult<bool> ResponseSuccessFailure(this bool isOk, string message = "")
        {
            if (!isOk && message.IsNullOrWhiteSpace()) message = ApiCodeEnum.SqlError.GetEnumDes();
            return new ApiResult<bool>
            {
                Code = isOk ? ApiCodeEnum.Success.GetEnumInt() : ApiCodeEnum.Unknown.GetEnumInt(),
                Msg = !string.IsNullOrWhiteSpace(message) ? message : ApiCodeEnum.Success.GetEnumDes(),
                IsOk = isOk,
                Data = isOk
            };
        }

        public static ApiResult<string> ResponseUnknown(this object obj, string message = "")
        {
            ApiResult<string> apiResult = new ApiResult<string>
            {
                Code = ApiCodeEnum.Unknown.GetEnumInt(),
                Msg = !string.IsNullOrWhiteSpace(message) ? message : ApiCodeEnum.Unknown.GetEnumDes(),
                IsOk = false,
                Data = null
            };
            return apiResult;
        }

        public static ApiResult<string> ResponseDataError(this object obj, string message = "")
        {
            ApiResult<string> apiResult = new ApiResult<string>
            {
                Code = ApiCodeEnum.DataError.GetEnumInt(),
                Msg = !string.IsNullOrWhiteSpace(message) ? message : ApiCodeEnum.DataError.GetEnumDes(),
                IsOk = false,
                Data = null
            };
            return apiResult;
        }

        public static ApiResult<string> ResponseNotLogin(this object obj, string message = "")
        {
            ApiResult<string> apiResult = new ApiResult<string>
            {
                Code = ApiCodeEnum.NotLogin.GetEnumInt(),
                Msg = !string.IsNullOrWhiteSpace(message) ? message : ApiCodeEnum.NotLogin.GetEnumDes(),
                IsOk = false,
                Data = null
            };
            return apiResult;
        }

        public static ApiResult<string> ResponseUnauthorized(this object obj, string message = "")
        {
            ApiResult<string> apiResult = new ApiResult<string>
            {
                Code = ApiCodeEnum.Unauthorized.GetEnumInt(),
                Msg = !string.IsNullOrWhiteSpace(message) ? message : ApiCodeEnum.Unauthorized.GetEnumDes(),
                IsOk = false,
                Data = null
            };
            return apiResult;
        }

        public static ApiResult<string> ResponseSignError(this object obj, string message = "")
        {
            ApiResult<string> apiResult = new ApiResult<string>
            {
                Code = ApiCodeEnum.SignError.GetEnumInt(),
                Msg = !string.IsNullOrWhiteSpace(message) ? message : ApiCodeEnum.SignError.GetEnumDes(),
                IsOk = false,
                Data = null
            };
            return apiResult;
        }
    }

    /// <summary>
    /// 消息实体
    /// </summary>
    public class ApiResult<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsOk { get; set; }

        /// <summary>
        /// 状态代码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 分页数量
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Msg { get; set; }
    }

    public enum ApiCodeEnum
    {
        [Description("成功")]
        Success = 200,
        [Description("未知错误")]
        Unknown = 500,
        [Description("请求数据错误")]
        DataError = 510,
        [Description("数据操作错误")]
        SqlError = 520,
        [Description("未登录")]
        NotLogin = 600,
        [Description("未授权")]
        Unauthorized = 610,
        [Description("签名错误")]
        SignError = 700
    }

    public enum YesOrNo
    {
        [Description("否")]
        No,
        [Description("是")]
        Yes
    }
}
