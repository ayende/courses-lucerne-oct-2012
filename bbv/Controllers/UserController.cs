using System;
using bbv.Models;

namespace bbv.Controllers
{
    public class UserController : RavenController
    {
         public object New(string name, string email, string nicks, string hobbies)
         {
             var user = new User
                            {
                                Name = name,
                                Email = email,
                            };
             user.Nicks.AddRange(nicks.Split(new []{","}, StringSplitOptions.RemoveEmptyEntries));
             user.Hobbies.AddRange(hobbies.Split(new []{","}, StringSplitOptions.RemoveEmptyEntries));
             
             Session.Store(user);

             return Json(user.Id);
         }
    }
}