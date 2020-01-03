using System;
using System.Collections.Generic;

namespace MyNavigationMenu.Model
{
    public class Perssitance
    {
        private static List<User> userList = new List<User>
        {
            //new User("bader", "bader"),
            //new User("admin", "admin")
        };

        public static User getUser(User user)
        {
            
            foreach(User aUser in Perssitance.userList)
            {
                
                if (user.username.Equals(aUser.username) && user.password.Equals(aUser.password))
                {
                    return user;
                }
            }
            return null;
        }
        
    }
}
