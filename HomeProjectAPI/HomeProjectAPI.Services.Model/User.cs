using System;

namespace HomeProjectAPI.Services.Model
{
    public class User
    {
        #region DB Properties
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }

        #endregion
        
        #region Other Properties

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        
        public Address Address { get; set; }
        #endregion
    }
}