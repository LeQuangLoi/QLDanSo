using System;

namespace HTTTQLDanSo.DataManagerment.DataModel
{
    public class FamilyPlanningHistory
    {
        public int FPHistory_ID { get; set; }

        public DateTime Contra_Date { get; set; }

        public int Contraceptive_Code { get; set; }

        public bool Export_Status { get; set; }

        public string Region_ID { get; set; }

        public int Personal_ID { get; set; }

        public int User_ID { get; set; }

        public DateTime Date_Update { get; set; }
    }
}