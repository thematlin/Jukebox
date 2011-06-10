using System;
using System.Collections.Generic;
using DotNetOpenAuth.OAuth.ChannelElements;
using DotNetOpenAuth.OAuth.Messages;
using DotNetOpenAuth.OpenId.Extensions.OAuth;

namespace Jukebox.Web.Code
{
    public class InMemoryTokenManager : IConsumerTokenManager, IOpenIdOAuthTokenManager
    {
        private Dictionary<string, string> tokensAndSecrets = new Dictionary<string, string>();

        public InMemoryTokenManager(string consumerKey, string consumerSecret)
        {
            if (String.IsNullOrEmpty(consumerKey))
                throw new ArgumentNullException("consumerKey");

            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
        }

        public string GetTokenSecret(string token)
        {
            return tokensAndSecrets[token];
        }

        public void StoreNewRequestToken(UnauthorizedTokenRequest request, ITokenSecretContainingMessage response)
        {
            tokensAndSecrets[response.Token] = response.TokenSecret;
        }

        public void ExpireRequestTokenAndStoreNewAccessToken(string consumerKey, string requestToken, string accessToken, string accessTokenSecret)
        {
            tokensAndSecrets.Remove(requestToken);
            tokensAndSecrets[accessToken] = accessTokenSecret;
        }

        public TokenType GetTokenType(string token)
        {
            throw new NotImplementedException();
        }

        public string ConsumerKey { get; private set; }

        public string ConsumerSecret { get; private set; }

        public void StoreOpenIdAuthorizedRequestToken(string consumerKey, AuthorizationApprovedResponse authorization)
        {
            tokensAndSecrets[authorization.RequestToken] = String.Empty;
        }
    }
}
