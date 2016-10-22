using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OpenIddict;
using Zervo.Data.Repositories.Contracts;
using Zervo.Domain.Services.Contracts;
using Zervo.Data.Models;
using Zervo.Extensions;

namespace Zervo.Identity
{
    public class TokenStore : IOpenIddictTokenStore<Token>
    {
        private readonly ITokenService _service;

        public TokenStore(ITokenService service)
        {
            _service = service;
        }

        #region IOpenIddictTokenStore

        /// <summary>
        /// Creates a new token, which is not associated with a particular user or client.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> CreateAsync(string type, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentException("The token type cannot be null or empty.");
            }

            var token = new Token { Type = type };
            int id = _service.Create(token);

            return Task.FromResult(id.ToString());
        }

        /// <summary>
        /// Retrieves an token using its unique identifier.
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Token> FindByIdAsync(string identifier, CancellationToken cancellationToken)
        {
            int id = identifier.TryParseInt();
            return Task.FromResult(_service.Get(id));
        }

        /// <summary>
        /// Revokes a token.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task RevokeAsync(Token token, CancellationToken cancellationToken)
        {
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            _service.Delete(token.Id);
            return Task.FromResult<object>(null);
        }

        #endregion
    }
}
