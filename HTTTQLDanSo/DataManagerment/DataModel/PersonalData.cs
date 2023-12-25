using System;

namespace HTTTQLDanSo.DataManagerment.DataModel
{
    public class PersonalData
    {
        public int ID { get; set; }

        public string FullName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int HouseholdID { get; set; }
    }
}