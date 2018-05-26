using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using ComcastCustomer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ComcastCustomer.Controllers
{
    [Produces("application/json")]
    public class CustomerController : Controller
    {
        #region "Constructor"
        public CustomerController()
        {

        }
        #endregion

        #region "Public Endpoints"
        /// <summary>
        /// This method would return all the customers from the dynamo db table
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getall")]
        public IEnumerable<string> Get()
        {
            //return GetAllItems(GetDynamoDbContext());
            return new string[] { "value1","value2"};
        }

        /// <summary>
        /// This method would return the customer with the specific customer id from the dynamo db table
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getcustomer/{id}")]
        public string Get(int id)
        {
            //return GetItem(GetTableObject(), id);
            return id.ToString();
        }

        /// <summary>
        /// This method would insert a new customer record into the dynamo db table
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createCustomer")]
        public HttpResponseMessage Post([FromBody] ComcastCustomerModel customerInfo)
        {
            this.InsertNewRecord(customerInfo);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        #endregion

        #region "PrivateMethods"
        /// <summary>
        /// This method returns the Dynamo db table object
        /// </summary>
        /// <returns>Dynamo DB table</returns>
        private Table GetTableObject()
        {
            string tableName = "ComcastCustomer";
            Table table;
            try
            {
                table = Table.LoadTable(GetDbClient(), tableName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n Error: failed to load the 'Movies' table; " + ex.Message);
                return (Table)null;
            }
            return table;
        }

        /// <summary>
        /// This method would return the dynamo DB context
        /// </summary>
        /// <returns>DynamoDbcontext</returns>
        private DynamoDBContext GetDynamoDbContext()
        {
            return new DynamoDBContext(GetDbClient());
        }

        /// <summary>
        /// This method would return the record from the dynamo db table that matches the specific key
        /// </summary>
        /// <param name="customerTable"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private async Task<string> GetItem(Table customerTable, int key)
        {
            Document document = await customerTable.GetItemAsync(key);
            Document doc = document;
           
            if (doc != null)
            {
                return doc.ToJsonPretty();
            }
            else
            {
                return string.Empty;
            }                        
        }

        /// <summary>
        /// This method would return all the items from the dynamo db table
        /// </summary>
        /// <param name="customerTableContext"></param>
        /// <returns>List of table items in JSON string format</returns>
        private async Task<string> GetAllItems(DynamoDBContext customerTableContext)
        {
            List<ScanCondition> conditions = new List<ScanCondition>();
            List<ComcastCustomerModel> comcastCustomerModelList = await customerTableContext.ScanAsync<ComcastCustomerModel>(conditions, (DynamoDBOperationConfig)null).GetRemainingAsync(new CancellationToken());
            List<ComcastCustomerModel> allDocs = comcastCustomerModelList;
            comcastCustomerModelList = (List<ComcastCustomerModel>)null;
            return allDocs == null ? string.Empty : JsonConvert.SerializeObject((object)allDocs);
        }

        /// <summary>
        /// This method would return the Dynamo DB client
        /// </summary>
        /// <returns>AmazonDynamoDbClient</returns>
        private AmazonDynamoDBClient GetDbClient()
        {
            AmazonDynamoDBConfig config = new AmazonDynamoDBConfig();
            try
            {
                return new AmazonDynamoDBClient(config);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n Error: failed to create a DynamoDB client; " + ex.Message);
                return (AmazonDynamoDBClient)null;
            }
        }

        /// <summary>
        /// This method will insert a new record in to the dynamo db table
        /// </summary>
        /// <param name="newCustomer"></param>
        private void InsertNewRecord(ComcastCustomerModel newCustomer)
        {
            Table tableObject = GetTableObject();
            Document doc = new Document();
            doc["CustomerId"] = newCustomer.CustomerId;
            doc["Name"] = newCustomer.Name;
            doc["Dob"] = newCustomer.Dob;
            doc["Address"] = newCustomer.Address;
            doc["City"] = newCustomer.City;
            doc["State"] = newCustomer.State;
            tableObject.PutItemAsync(doc);
        }
        #endregion



    }
}
