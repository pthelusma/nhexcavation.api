using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using walterblacc.lib.DomainObjects;
using walterblacc.lib.DTO;
using walterblacc.lib.Repository;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace walterblacc
{
	public class LambdaHandler
	{
		public LambdaHandler()
		{
		}

		static void Main(string[] args)
		{
			Task.Run(async () =>
			{
				using (var stream = new FileStream($"{Directory.GetCurrentDirectory()}/test.data.json", FileMode.Open))
				{
					var serializer = new Amazon.Lambda.Serialization.Json.JsonSerializer();
					CommunicationRequest communication = serializer.Deserialize<CommunicationRequest>(stream);

					var response = await new LambdaHandler().Handle(communication, null);

					Console.WriteLine(response);
				}
			}).GetAwaiter().GetResult();
		}

		public async Task<dynamic> Handle(CommunicationRequest response, ILambdaContext lambdaContext)
		{
			response.lambdaContext = lambdaContext;
			var repository = new DynamoDbRepository();
			var communication = mapper(response);

			return await repository.save(communication);
		}

		public Communication mapper(CommunicationRequest response) 
		{
			return new Communication()
			{
				Id = response.lambdaContext == null ? Guid.NewGuid().ToString() : response.lambdaContext.AwsRequestId,
				FirstName = response.FirstName,
				LastName = response.LastName,
				Address1 = response.Address1,
				Address2 = response.Address2,
				City = response.City,
				State = response.State,
				ZipCode = response.ZipCode,
				Email = response.Email,
				PhoneNumber = response.PhoneNumber,
				CreateDate = response.CreateDate
			};
		}
			
	}
}

