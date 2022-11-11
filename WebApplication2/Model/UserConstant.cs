using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jwt.Model
{
    public class UserConstant
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel {Username = "mangal singh",EmailAddress = "singhmangal.s.p@gmail.com",Password="mangal",Role="admin"
            ,Surname="singh",GivenName="mangal"
            },
             new UserModel {Username = "rahul singh",EmailAddress = "rahul.s.p@gmail.com",Password="rahul",Role="seller"
            ,Surname="singh",GivenName="rahul"
            },
        };
    }
}
