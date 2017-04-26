using System;
using Amazon.DynamoDBv2.DataModel;

namespace walterblacc.lib.DomainObjects
{
    [DynamoDBTable("Communication")]
    public class Communication
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string ZipCode { get; set; }
		public string PhoneNumber { get; internal set; }
		public string Email { get; internal set; }
		public DateTime CreateDate { get; set; }
		public string CreateBy { get { return "System"; } }
    }
}

