using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainScheme1
{
    class UserManager
    {
        private List<User> users = new List<User>();
        private WebRequest request = new WebRequest("http://192.168.50.6");

        public UserManager()
        {
            List<string> uidList = request.GetUserList();
            //List<string> uidList = new List<string> { "9", "20", "8", "23", "10", "15", "20", "69", "87", "46", "11", "22", "41", "1", "2", "17"};

            while(uidList.Count > 0) {
                User _user = new User(uidList[0],Convert.ToInt32(uidList[1]));
                
                users.Add(_user);

                uidList.RemoveAt(0);
                uidList.RemoveAt(0);
            }
        }




        public User GetUser(string UID)
        {
            User _user = null;
            foreach (User u in users)
            {
                if (u.ToString() == UID)
                {
                    _user = u;
                }
            }
            if (_user == null)
            {
                _user = new User(UID);
                users.Add(_user);
                request.NewUser(UID, "20", "0");
            }
            return _user;
        }
    }


}
