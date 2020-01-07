using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainScheme1
{
    class UserManager
    {
        List<User> users = new List<User>();

        public User GetUser(string UID)
        {
            User _user = null;
            foreach (User u in users)
            {
                if (u.ToString() == UID )
                    {
                    _user = u;

                }
               
            }
            if (_user == null)
            {
                _user = new User(UID);
                users.Add(_user);
            }
            return _user;

        }
    }

   
}
