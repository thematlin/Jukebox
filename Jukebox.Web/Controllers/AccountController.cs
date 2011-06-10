using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OAuth.Messages;
using Jukebox.Business.Models;
using Jukebox.Infrastructure.Membership;
using Jukebox.Infrastructure.Repositories;
using Jukebox.Web.Code;
using Jukebox.Web.Models;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OAuth.ChannelElements;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.ApplicationBlock;

namespace Jukebox.Web.Controllers
{
    public class AccountController : Controller
    {
        private static readonly OpenIdRelyingParty Openid = new OpenIdRelyingParty();

        private readonly IFormsAuthentication _formsAuth;
        private readonly IUserRepository _userRepository;
        private readonly IConsumerTokenManager _consumerTokenManager;

        public AccountController(IFormsAuthentication formsAuth, IUserRepository userRepository, IConsumerTokenManager consumerTokenManager)
        {
            _formsAuth = formsAuth;
            _userRepository = userRepository;
            _consumerTokenManager = consumerTokenManager;
        }

        public ActionResult LogOn()
        {
            return View();
        }

        public ActionResult OAuth(string returnUrl)
        {
            var twitter = new WebConsumer(TwitterConsumer.ServiceDescription, _consumerTokenManager);
            var url = Request.Url.ToString().Replace("OAuth", "OAuthCallback");
            var callbackUri = new Uri(url);
            twitter.Channel.Send(twitter.PrepareRequestUserAuthorization(callbackUri, null, null));

            return RedirectToAction("LogOn");
        }

        public ActionResult OAuthCallback()
        {
            var twitter = new WebConsumer(TwitterConsumer.ServiceDescription, _consumerTokenManager);
            var accessTokenResponse = twitter.ProcessUserAuthorization();

            if (accessTokenResponse != null)
            {
                string userName = accessTokenResponse.ExtraData["screen_name"];
                return CreateUser(userName, null, null, null);
            }

            return RedirectToAction("LogOn");
        }

        public ActionResult OpenId(string openIdUrl)
        {
            var response = Openid.GetResponse();
            if (response == null)
            {
                Identifier id;
                if (Identifier.TryParse(openIdUrl, out id))
                {
                    try
                    {
                        var request = Openid.CreateRequest(openIdUrl);
                        var fetch = new FetchRequest();
                        fetch.Attributes.AddRequired(WellKnownAttributes.Contact.Email);
                        fetch.Attributes.AddRequired(WellKnownAttributes.Name.First);
                        fetch.Attributes.AddRequired(WellKnownAttributes.Name.Last);
                        request.AddExtension(fetch);

                        return request.RedirectingResponse.AsActionResult();
                    }
                    catch (ProtocolException ex)
                    {
                        throw ex;
                    }
                }

                return RedirectToAction("LogOn");
            }

            switch (response.Status)
            {
                case AuthenticationStatus.Authenticated:
                    var fetch = response.GetExtension<FetchResponse>();
                    string firstName = "";
                    string lastName = "";
                    string email = "";
                    if (fetch != null)
                    {
                        firstName = fetch.GetAttributeValue(WellKnownAttributes.Name.First);
                        lastName = fetch.GetAttributeValue(WellKnownAttributes.Name.Last);
                        email = fetch.GetAttributeValue(WellKnownAttributes.Contact.Email);
                    }
                    return CreateUser(response.ClaimedIdentifier, firstName, lastName, email);
                default:
                    return RedirectToAction("LogOn");
            }
        }

        private ActionResult CreateUser(string username, string firstName, string lastName, string email)
        {
            var user = _userRepository.Query().FirstOrDefault(x => x.UserName.Equals(username));

            if (user == null)
            {
                user = new User
                           {
                               Name = firstName + " " + lastName,
                               UserName = username
                           };

                return View("Register", user);
            }

            _formsAuth.SignIn(user.UserName, false);

            return RedirectToAction("Index", "Library");
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (_userRepository.Query().FirstOrDefault(x => x.UserName.Equals(user.UserName)) == null)
            {
                _userRepository.AddUser(user);
            }

            _formsAuth.SignIn(user.UserName, false);

            return RedirectToAction("Index", "Library");
        }
        
        public ActionResult LogOff()
        {
            _formsAuth.SignOut();

            return RedirectToAction("Index", "Library");
        }
    }
}
