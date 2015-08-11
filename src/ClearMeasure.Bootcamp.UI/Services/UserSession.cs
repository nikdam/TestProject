using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Web;
using ClearMeasure.Bootcamp.Core;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Plugins.DataAccess;
using ClearMeasure.Bootcamp.Core.Services;
using System;

namespace ClearMeasure.Bootcamp.UI.Services
{
    public class UserSession : IUserSession
    {
        private readonly Bus _bus;

        public UserSession(Bus bus)
        {
            _bus = bus;
        }

        // todo: target for MVC6 rework
        //public UserSession() : this(DependencyResolver.Current.GetService<Bus>())
        //{
        //}

        #region IUserSession Members

        public Employee GetCurrentUser()
        {
            // todo: target for MVC6 rework
            throw new NotImplementedException();

            //IOwinContext context = HttpContext.Current.GetOwinContext();
            //ClaimsPrincipal user = context.Authentication.User;
            //if (!user.Identity.IsAuthenticated)
            //{
            //    return null;
            //}

            //string userName = user.Claims.Single(claim => claim.Type == ClaimTypes.Name).Value;
            //Employee currentUser = _bus.Send(new EmployeeByUserNameQuery(userName)).Result;
            //blowUpIfEmployeeCannotLogin(currentUser);
            //return currentUser;
        }

        public void LogIn(Employee employee)
        {
            blowUpIfEmployeeCannotLogin(employee);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, employee.UserName),
                new Claim(ClaimTypes.Email, employee.EmailAddress)
            };

            // todo: target for MVC6 rework
            throw new NotImplementedException();

            //var id = new ClaimsIdentity(claims,
            //                            DefaultAuthenticationTypes.ApplicationCookie);

            //var context = HttpContext.Current.GetOwinContext();
            //var authenticationManager = context.Authentication;
            //authenticationManager.SignIn(id);
        }

        public void LogOut()
        {
            // todo: target for MVC6 rework
            throw new NotImplementedException();

            //var context = HttpContext.Current.GetOwinContext();
            //var authenticationManager = context.Authentication;
            //authenticationManager.SignOut();
        }

        #endregion

        private void blowUpIfEmployeeCannotLogin(Employee employee)
        {
            if (employee == null)
            {
                throw new InvalidCredentialException("That user doesn't exist or is not valid.");
            }
        }
    }
}