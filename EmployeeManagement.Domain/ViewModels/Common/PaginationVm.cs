using System.Runtime.Serialization;

namespace EmployeeManagement.Domain.ViewModels.Common
{
    [DataContract]
    public class PaginationVm
    {
        [DataMember]
        public int PageSize { get; set; }
        [DataMember]
        public int PageNumber { get; set; }
    }
}
