using System;

namespace HTTTQLDanSo.DataManagerment.DataModel
{
    public class GenerateHealth
    {
        public int Generate_ID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string DateCreated
        {
            get
            {
                return CreatedDate.HasValue ? CreatedDate.Value.ToString("dd/MM/yyyy") : string.Empty;
            }
        }

        public int Generate_Code { get; set; }

        public int? Birth_Number { get; set; }

        public int BirthNumber
        {
            get
            {
                return Birth_Number.HasValue ? Birth_Number.Value : 0;
            }
        }

        public string PlaceOfBirth { get; set; }

        public string Deliver { get; set; }

        public string GenName
        {
            get
            {
                switch (Generate_Code)
                {
                    case 1:
                        return "Sinh Con";

                    default:
                        return "";
                }
            }
        }

        public string POB
        {
            get
            {
                switch (PlaceOfBirth)
                {
                    case "S":
                        return "Tại cơ sở y tế";

                    case "H":
                        return "Tại nhà";

                    case "O":
                        return "Tại nơi khác";

                    default:
                        return "";
                }
            }
        }

        public string GenDate
        {
            get
            {
                return Gen_Date.ToString("dd/MM/yyyy");
            }
        }

        public bool ResultSLTS1
        {
            get
            {
                switch (Result_SLTS1)
                {
                    case "DT":
                        return true;

                    case "AT":
                        return true;

                    default:
                        return false;
                }
            }
        }

        public string DateSLTS1
        {
            get
            {
                return Date_SLTS1.HasValue ? Date_SLTS1.Value.ToString("dd/MM/yyyy") : "";
            }
        }

        public DateTime? Date_SLTS1 { get; set; }

        public string Result_SLTS1 { get; set; }

        public bool ResultSLTS2
        {
            get
            {
                switch (Result_SLTS2)
                {
                    case "DT":
                        return true;

                    case "AT":
                        return true;

                    default:
                        return true;
                }
            }
        }

        public string DateSLTS2
        {
            get
            {
                return Date_SLTS2.HasValue ? Date_SLTS2.Value.ToString("dd/MM/yyyy") : "";
            }
        }

        public DateTime? Date_SLTS2 { get; set; }

        public string Result_SLTS2 { get; set; }

        public bool ResultSLTS
        {
            get
            {
                switch (Result_SLTS)
                {
                    case "DT":
                        return true;

                    case "AT":
                        return true;

                    default:
                        return false;
                }
            }
        }

        public string Result_SLTS { get; set; }

        public DateTime? Date_SLSS { get; set; }

        public string DateSLSS
        {
            get
            {
                return Date_SLSS.HasValue ? Date_SLSS.Value.ToString("dd/MM/yyyy") : "";
            }
        }

        public DateTime Gen_Date { get; set; }
    }
}