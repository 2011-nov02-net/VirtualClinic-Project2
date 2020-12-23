using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualClinic.Domain.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        private string _type;
        public string Type {
            get
            {
                return _type;
            }
            set
            {
                if(value.Equals("doctor") || value.Equals("patient")){
                    _type = value;
                }
                else
                {
                    throw new ArgumentException("Invalid type");
                }
            }
        }

        public string Email { get; set; }


        public UserModel(int id, string email, string type="doctor")
        {
            this.Id = id;
            this.Email = email;
            this.Type = type;
        }
    }
}
