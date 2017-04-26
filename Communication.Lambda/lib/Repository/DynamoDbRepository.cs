using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using walterblacc.lib.DomainObjects;
using walterblacc.lib.DTO;

namespace walterblacc.lib.Repository
{
	public class DynamoDbRepository : IRepository
	{
		DynamoDBContext GetDynamoDBContext()
		{
			return new DynamoDBContext(GetAmazonDynamoDBClient());
		}

		AmazonDynamoDBClient GetAmazonDynamoDBClient()
		{
			var awsCredentials = GetAWSCredentials();

			return awsCredentials == null ? new AmazonDynamoDBClient(Amazon.RegionEndpoint.USEast1) :
				new AmazonDynamoDBClient(awsCredentials, Amazon.RegionEndpoint.USEast1);
		}

		AWSCredentials GetAWSCredentials()
		{
			var chain = new CredentialProfileStoreChain();
			AWSCredentials awsCredentials;
			if (chain.TryGetAWSCredentials("personal", out awsCredentials))
			{
				return awsCredentials;
			}

			return null;
		}

		public async Task<dynamic> save(Communication communication)
		{
			var dbContext = GetDynamoDBContext();

			await dbContext.SaveAsync(communication);

			return new {
				Message = "Success",
				OperationType = "Save",
			};
		}
	}
}