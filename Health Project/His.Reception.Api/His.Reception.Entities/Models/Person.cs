using System;
using System.Collections.Generic;

namespace His.Reception.Entities.Models
{
    public partial class Person
    {
        public Person()
        {
            Doctors = new HashSet<Doctors>();
            Patient = new HashSet<Patient>();
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string NationalCode { get; set; }
        public int? SexId { get; set; }
        public string ShNo { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? MaritalStatusId { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public short? Age { get; set; }
        public string Email { get; set; }
        public int? BirthPlaceId { get; set; }
        public string Address { get; set; }

        public virtual BirthPlace BirthPlace { get; set; }
        public virtual MaritalStatus MaritalStatus { get; set; }
        public virtual Sex Sex { get; set; }
        public virtual ICollection<Doctors> Doctors { get; set; }
        public virtual ICollection<Patient> Patient { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
