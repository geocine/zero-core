using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Zervo.GraphQL;

namespace Zervo.Controllers
{
    [Route("api/[controller]")]
    public class GraphQLController : Controller
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _executer;
        private readonly IDocumentWriter _writer;
        private readonly IDictionary<string, string> _namedQueries;

        public GraphQLController(
            IDocumentExecuter executer,
            IDocumentWriter writer,
            ISchema schema)
        {
            _executer = executer;
            _writer = writer;
            _schema = schema;
        }

        [HttpPost]
        public async Task<ContentResult> PostAsync(HttpRequestMessage request, [FromBody]GraphQLQuery query)
        {
            var inputs = query.Variables.ToInputs();
            var queryToExecute = query.Query;

            // Operation Name should match that on the Query
            var result = await ExecuteAsync(_schema, null, queryToExecute, query.OperationName, inputs).ConfigureAwait(true);

            var httpResult = result.Errors?.Count > 0
                ? HttpStatusCode.BadRequest
                : HttpStatusCode.OK;

            var json = _writer.Write(result);

            return new ContentResult
            {
                Content = json,
                ContentType = "application/json",
                StatusCode = (int)httpResult
            };
        }

        public Task<ExecutionResult> ExecuteAsync(
          ISchema schema,
          object rootObject,
          string query,
          string operationName = null,
          Inputs inputs = null)
        {
            return _executer.ExecuteAsync(schema, rootObject, query, operationName, inputs);
        }
    }
}
