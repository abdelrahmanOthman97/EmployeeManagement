using EmployeeManagement.Common.Enum;
using System.Runtime.Serialization;

namespace EmployeeManagement.Domain.ViewModels.Common
{
    [DataContract]
    public class BaseResponse
    {
        public BaseResponse()
          : this(0, string.Empty, string.Empty)
        {
        }

        public BaseResponse(int errorCode, string successMsg, string errorMsg)
            : this(errorCode, successMsg, errorMsg, string.Empty)
        {
        }

        public BaseResponse(int errorCode, string successMsg, string errorMsg, string errorDetails)
        {
            ResponseCode = (ResponseCodeEnum)errorCode;
            SuccessMsg = successMsg;
            ErrorMsg = errorMsg;
            ErrorDetails = errorDetails;
        }

        public bool IsSucceeded => this.ResponseCode == ResponseCodeEnum.Success;

        [DataMember]
        public ResponseCodeEnum ResponseCode { get; set; }

        [DataMember]
        public string SuccessMsg { get; set; }
        [DataMember]
        public string ErrorMsg { get; set; }

        [DataMember]
        public string ErrorDetails { get; set; }

        [DataMember]
        public int TotalSeconds { get; set; }

    }
}
