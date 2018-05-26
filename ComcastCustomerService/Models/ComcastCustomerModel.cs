using Amazon.DynamoDBv2.DataModel;

namespace ComcastCustomer.Models
{
    [DynamoDBTable("ComcastCustomer")]
    public class ComcastCustomerModel
    {
        [DynamoDBHashKey]
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string Dob { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }
    }
}
