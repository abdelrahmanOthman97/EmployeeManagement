using System.Runtime.Serialization;

namespace EmployeeManagement.Domain.ViewModels.Common
{
    [DataContract]
    public class Response<T> : BaseResponse
    {
        public Response()
        {
        }

        [DataMember]
        public T Data { get; set; }
    }
}
