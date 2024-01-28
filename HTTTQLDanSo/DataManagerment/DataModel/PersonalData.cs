using System;

namespace HTTTQLDanSo.DataManagerment.DataModel
{
    public class PersonalData
    {
        public int Personal_ID { get; set; }

        public string Full_Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int HouseholdID { get; set; }
    }
}