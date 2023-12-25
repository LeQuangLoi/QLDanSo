using System;

namespace HTTTQLDanSo.DataManagerment.DataModel
{
    public class PersonalInfo
    {
        public int HouseHold_ID { get; set; }

        public int Personal_ID { get; set; }

        public string Full_Name { get; set; }

        public string DateOfBirth { get; set; }

        public string DOB { get; set; }

        public int Sex_ID { get; set; }

        public string Sex_Name { get; set; }

        public string Relation_Code { get; set; }

        public string Relation_Name { get; set; }

        public string Residence_Code { get; set; }

        public string Residence_Name { get; set; }

        public string Technical_Code { get; set; }

        public string Technical_Name { get; set; }

        public string Marital_Code { get; set; }

        public string Marital_Name { get; set; }

        public string Ethnic_Code { get; set; }

        public string Ethnic_Name { get; set; }

        public string Education_Code { get; set; }

        public string Invalid_Code { get; set; }

        public string Education_Level { get; set; }

        public string Education_Name { get; set; }

        public string Person_Status { get; set; }

        public int Mother_ID { get; set; }

        public string Birth_Number { get; set; }

        public DateTime Start_Date { get; set; }

        public int Generate_ID { get; set; }

        public string Cccd_Bhyt_Code { get; set; }

        public DateTime Change_Date { get; set; }

        public bool? inbreeding { get; set; }
    }
}