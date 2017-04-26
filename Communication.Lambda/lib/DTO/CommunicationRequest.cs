using System;
using Amazon.Lambda.Core;

namespace walterblacc.lib.DTO
{
    public class CommunicationRequest
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string ZipCode { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public DateTime CreateDate { get; set; }
		public PreferredContactMethodEnum PreferredContactMethod { get; set; }
		public ILambdaContext lambdaContext { get; set; }
    }

	public enum PreferredContactMethodEnum { Email, Phone }
}

